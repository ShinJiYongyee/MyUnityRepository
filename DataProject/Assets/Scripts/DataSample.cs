using UnityEngine;

public class DataSample : MonoBehaviour
{
    //UserData�� �ʵ�μ� ����
    public UserData userData;

    public void Start()
    {
        //PlayerPrefs.SetString("ID", userData.UserID);
        //PlayerPrefs.SetString("UserName", userData.UserName);
        //PlayerPrefs.SetString("UserPassword", userData.UserPassword);
        //PlayerPrefs.SetString("UserEmail", userData.UserEmail);

        Debug.Log("�����Ͱ� ����Ǿ����ϴ�");

        //Debug.Log(PlayerPrefs.GetString("ID"));

        //PlayerPrefs.DeleteAll();    //������ ��ü ����

        //Debug.Log("�����͸� ���� �����߽��ϴ�");
    }

}
