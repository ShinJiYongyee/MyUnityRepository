using UnityEngine;

public class DataSample : MonoBehaviour
{
    //UserData를 필드로서 보유
    public UserData userData;

    public void Start()
    {
        //PlayerPrefs.SetString("ID", userData.UserID);
        //PlayerPrefs.SetString("UserName", userData.UserName);
        //PlayerPrefs.SetString("UserPassword", userData.UserPassword);
        //PlayerPrefs.SetString("UserEmail", userData.UserEmail);

        Debug.Log("데이터가 저장되었습니다");

        //Debug.Log(PlayerPrefs.GetString("ID"));

        //PlayerPrefs.DeleteAll();    //데이터 전체 삭제

        //Debug.Log("데이터를 전부 삭제했습니다");
    }

}
