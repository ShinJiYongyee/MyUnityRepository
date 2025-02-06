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

        //data02�κ��� ���ڿ� ���·� ���� ����
        string data_value = data02.GetData();
        Debug.Log(data_value);

        //������ ���ڿ� ������ ���� Ű Data01�� �Ҵ�
        PlayerPrefs.SetString("Data01",data_value);

        //data01 ��ü�� Ű Data01�� ���� �Ҵ�
        data01 = UserData.SetData(data_value);

        //�Ҵ���� data01�� ���� Ȯ��
        Debug.Log(data01.GetData());
    }

}

