using UnityEngine;

public class CreateObject2 : MonoBehaviour
{
    public GameObject prefab;

    private GameObject square;
    void Start()
    {
        square=Instantiate(prefab);

        Destroy(square, 5.0f);  //square를 생성 후 5초 뒤 파괴
        Debug.Log("파괴되었습니다.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
