using System.IO;
using UnityEngine;

//�ý����� ���� �� �ִ� Ŭ����
[System.Serializable]
public class Item01
{
    public string item_name;
    public string item_description;
} 


public class JsonLoadSample : MonoBehaviour
{
    void Start()
    {
        //��� �ؽ�Ʈ ���� �� Ư�� ��ο� �̸��� ������ �о����
        string load_json = 
            File.ReadAllText(Application.dataPath+"/item01.json");

        //json������ �о�� Ŭ������ ����ȯ
        var data=JsonUtility.FromJson<Item01>(load_json);

        //Ŭ������ �о�� json������ �а� �� �� �ִ�.
        Debug.Log(data);

        //Ŭ������ ������ json������ �ٲٱ�
        data.item_name = "��!��!��!";
        data.item_description = "��!��!��!";

        //item02.json�̶�� ������ json���Ϸ� ������ �����ϱ�
        File.WriteAllText(Application.dataPath+"/item02.json",
            JsonUtility.ToJson(data));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
