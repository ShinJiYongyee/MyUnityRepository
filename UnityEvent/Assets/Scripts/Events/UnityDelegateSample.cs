using System;
using UnityEngine;
using UnityEngine.UI;

class MeetEvent
{
    public delegate void MeetEventHandler(string message);
    //EventHandler�� delegate�̹Ƿ� 
    public event MeetEventHandler meetHandler;

    public void Meet()
    {
        meetHandler("���� �͵� �ο�");
    }

}
public class UnityDelegateSample : MonoBehaviour
{
    public Text messageUI;

    //�ʵ�� �� ������ Ÿ���� ��Ȯ�ؾ� �ϹǷ� var ��� �Ұ�
    MeetEvent meetEvent = new MeetEvent();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meetEvent.meetHandler += EventMessage;
    }

    //EventHandler�� ������ �Ű������� �Լ� �ڵ��ϼ�
    private void EventMessage(string message)
    {
        //Debug.Log(message);
        messageUI.text = message;
    }

    public void OnMeetButton()
    {
        meetEvent.Meet();
    }
}
