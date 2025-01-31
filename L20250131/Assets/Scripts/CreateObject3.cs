using UnityEngine;

public class CreateObject3 : MonoBehaviour
{
    [SerializeField]private GameObject prefab;
    private int dummy;
    //직렬화
    //데이터나 오브젝트를 재구성할 수 있는 형태(format)로 변환하는 과정
    //유니티에서 간단하게 private형태의 데이터를 Inspector에서 읽을 수 있게 설정해준다
    [SerializeField] GameObject sample;
    void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Table_Body");
        //Resources.Load<T>("폴더위치/에셋명")
        //T는 만들 데이터의 자료형
        //Resources 에서 시작하는 경로의 특정 오브젝트를 생성

    }
    void Update()
    {
        ///if문으로 키 입력받기        
        ///입력받은 키가 스페이스일 경우
        ///GetKeyDown(1회 입력)
        ///GetKeyUp(입력 후 떼기)
        ///GetKey(누르는 동안)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Sprite sprite = Resources.Load<Sprite>("Sprites/sprite01");
            //Resources/Sprites 아래 sprite01이 없으므로 구동되지 않음
            //엔진에서는 이를 식별하지 않음
            sample=Instantiate(prefab);
            sample.AddComponent<VectorSample>();
            //AddComponent<T>
            //오브젝트의 컴포넌트 기능을 얻어오는 기능
            //GetComponent<T>
            //오브젝트가 가지고 있는 컴포넌트 기능을 얻어오는 기능
            //스크립트에서 해당 컴포넌트의 기능을 가져와 사용하려 할 경우 사용
        }
    }
}
