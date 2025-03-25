using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TMPro;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class LoginPacket
{
    public string code;
    public string id;
    public string password;
}

[Serializable]
public class SighupPacket
{
    public string code;
    public string id;
    public string password;
    public string name;
    public string email;
}

public class NetworkManager : MonoBehaviour
{
    private Socket serverSocket;
    private IPEndPoint serverEndPoint;
    private Thread recvThread;

    public TMP_InputField newIdUI;
    public TMP_InputField newPasswordUI;
    public TMP_InputField nameUI;
    public TMP_InputField emailUI;

    public TMP_InputField idUI;
    public TMP_InputField passwordUI;

    void Start()
    {
        ConnectedToServer();
    }
    void RecvPacket()
    {
        if (serverSocket.Available > 0)
        {
            while (true)
            {
                byte[] lengthBuffer = new byte[2];

                int RecvLength = serverSocket.Receive(lengthBuffer, 2, SocketFlags.None);
                ushort length = BitConverter.ToUInt16(lengthBuffer, 0);
                length = (ushort)IPAddress.NetworkToHostOrder((short)length);
                byte[] recvBuffer = new byte[4096];
                RecvLength = serverSocket.Receive(recvBuffer, length, SocketFlags.None);

                string jsonString = Encoding.UTF8.GetString(recvBuffer);
                Debug.Log(jsonString);
                Thread.Sleep(10);
            }

        }
    }
    private void ConnectedToServer()
    {
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4000);
        serverSocket.Connect(serverEndPoint);
        recvThread = new Thread(new ThreadStart(RecvPacket));
        recvThread.IsBackground = true;
        recvThread.Start();
    }

    void SendPacket(string message)
    {
        byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
        ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messageBuffer.Length);

        byte[] headerBuffer = BitConverter.GetBytes(length);

        byte[] packetBuffer = new byte[headerBuffer.Length + messageBuffer.Length];
        Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
        Buffer.BlockCopy(messageBuffer, 0, packetBuffer, headerBuffer.Length, messageBuffer.Length);

        int SendLength = serverSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);
    }
    public void OnLogin()
    {
        LoginPacket packet = new LoginPacket();
        packet.code = "Login";
        packet.id = idUI.text;
        packet.password = passwordUI.text;

        SendPacket(JsonUtility.ToJson(packet));

        Debug.Log(packet);
    }
    public void OnSignup()
    {
        SighupPacket packet = new SighupPacket();
        packet.code = "Signup";
        packet.id = newIdUI.text;
        packet.password = newPasswordUI.text;
        packet.email = emailUI.text;
        packet.name = nameUI.text;

        SendPacket(JsonUtility.ToJson(packet));

        Debug.Log(packet);
    }
    private void OnApplicationQuit()
    {
        if(recvThread != null)
        {
            recvThread.Abort();
        }
        if(serverSocket != null)
        {
            serverSocket.Shutdown(SocketShutdown.Both); //연결 끊기 통보
            serverSocket.Close();
        }
    }
}
