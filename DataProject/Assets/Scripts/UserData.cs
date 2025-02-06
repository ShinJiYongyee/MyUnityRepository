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

    //void가 아닌 값을 반환하는 단일 문장 메소드 선언
    /// <summary>
    /// 데이터를 하나의 문자열로 반환하는 코드
    /// </summary>
    /// <returns>아이디, 이름, 비밀번호, 이메일</returns>
    public string GetData() => 
        $"{UserID}, {UserName},{UserPassword},{UserEmail}";

    /// <summary>
    /// 데이터에 대한 정보를 전달받고 UserData로 반환하는 코드
    /// </summary>
    /// <param name="data">아이디, 이름, 비밀번호, 이메일</param>
    /// <returns></returns>
    public static UserData SetData(string data)
    {
        string[] data_values = data.Split(',');
        //string 클래스의 메소드
        //문자열을 괄호 안 값을 기준으로 잘라 배열에 할당

        return new UserData(
            data_values[0], data_values[1], 
            data_values[2], data_values[3]);
    }
    
}
