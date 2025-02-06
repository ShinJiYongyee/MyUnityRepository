using UnityEngine;

//클래스 직렬화
//데이터를 읽을 수 있는 형태로 변환
[System.Serializable]
public class UserData 
{
    // 데이터로서만 사용하므로 MonoBehavior 제거
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
