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
}
