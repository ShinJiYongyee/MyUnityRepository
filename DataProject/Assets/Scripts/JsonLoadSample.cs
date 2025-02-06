using System.IO;
using UnityEngine;

//시스템이 읽을 수 있는 클래스
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
        //모든 텍스트 파일 중 특정 경로와 이름의 파일을 읽어오기
        string load_json = 
            File.ReadAllText(Application.dataPath+"/item01.json");

        //json파일을 읽어와 클래스로 형변환
        var data=JsonUtility.FromJson<Item01>(load_json);

        //클래스로 읽어온 json파일은 읽고 쓸 수 있다.
        Debug.Log(data);

        //클래스로 가져온 json데이터 바꾸기
        data.item_name = "쌀!쌀!쌀!";
        data.item_description = "우!우!우!";

        //item02.json이라는 별도의 json파일로 내보내 저장하기
        File.WriteAllText(Application.dataPath+"/item02.json",
            JsonUtility.ToJson(data));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
