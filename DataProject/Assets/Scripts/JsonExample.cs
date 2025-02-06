using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SampleData
{
    public int i;
    public float f;
    public bool b;
    public Vector3 v;
    public string s;
    public int[] iArray;
}
public class JsonExample : MonoBehaviour
{
    void Start()
    {
        SampleData sampleData = new SampleData();
        sampleData.i = 0;
        sampleData.f = 1.0f;
        sampleData.b = false;
        sampleData.v = Vector3.zero;
        sampleData.s = "hello";
        sampleData.iArray = new int[] { 1, 2, 3, 4, 5 };
        //sampleData클래스를 통해 만든 (string)json 파일
        string json_data = JsonUtility.ToJson(sampleData);
        Debug.Log(json_data);
        //json_data를 통해 전달받은 값으로 만든 SampleData 객체
        var sampleData2 = JsonUtility.FromJson<SampleData>(json_data);
        Debug.Log(sampleData2.s);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
