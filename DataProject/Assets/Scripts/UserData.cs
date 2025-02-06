using UnityEngine;

//Ŭ���� ����ȭ
//�����͸� ���� �� �ִ� ���·� ��ȯ
[System.Serializable]
public class UserData 
{
    // �����ͷμ��� ����ϹǷ� MonoBehavior ����
    public string UserID;
    public string UserName;
    public string UserPassword;
    public string UserEmail;

    public UserData() 
    {
        
    }
    public UserData(string userID, 
        string userName, 
        string userPassword, string userEmail)
    {
        UserID = userID;
        UserName = userName;
        UserPassword = userPassword;
        UserEmail = userEmail;
    }

    //void�� �ƴ� ���� ��ȯ�ϴ� ���� ���� �޼ҵ� ����
    /// <summary>
    /// �����͸� �ϳ��� ���ڿ��� ��ȯ�ϴ� �ڵ�
    /// </summary>
    /// <returns>���̵�, �̸�, ��й�ȣ, �̸���</returns>
    public string GetData() => 
        $"{UserID}, {UserName},{UserPassword},{UserEmail}";

    /// <summary>
    /// �����Ϳ� ���� ������ ���޹ް� UserData�� ��ȯ�ϴ� �ڵ�
    /// </summary>
    /// <param name="data">���̵�, �̸�, ��й�ȣ, �̸���</param>
    /// <returns></returns>
    public static UserData SetData(string data)
    {
        string[] data_values = data.Split(',');
        //string Ŭ������ �޼ҵ�
        //���ڿ��� ��ȣ �� ���� �������� �߶� �迭�� �Ҵ�

        return new UserData(
            data_values[0], data_values[1], 
            data_values[2], data_values[3]);
    }
    
}
