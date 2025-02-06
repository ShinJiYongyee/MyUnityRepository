using UnityEngine;

public class UserDataSystem : MonoBehaviour
{
    public UserData data01;
    public UserData data02;

    private void Start()
    {
        data01=new UserData();
        data02=new UserData(
            "sample01", "choi", "asd123","choi012@naver.com");

        //data02로부터 문자열 형태로 값을 추출
        string data_value = data02.GetData();
        Debug.Log(data_value);

        //추출한 문자열 형태의 값을 키 Data01에 할당
        PlayerPrefs.SetString("Data01",data_value);

        //data01 객체에 키 Data01의 값을 할당
        data01 = UserData.SetData(data_value);

        //할당받은 data01의 값을 확인
        Debug.Log(data01.GetData());
    }

}

