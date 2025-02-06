using UnityEngine;

public class UserDataSystem : MonoBehaviour
{
    public UserData data01;
    public UserData data02;

    private void Start()
    {
        data01=new UserData();
        data02=new UserData("sample01", "choi", "asd123","choi012@naver.com");
    }

}
