using UnityEngine;

public class CreateObject : MonoBehaviour
{
    public GameObject prefab;
    void Start()
    {
        Instantiate(prefab);    
        //생성 기능 Instantiate();
        //등록한 prefab 그대로 생성

        Instantiate(prefab,new Vector3(0,0,0),Quaternion.identity);
        //위치를 지정해 생성, 회전값 적용
        //Vector3: 방향과 크기를 설명하는 개념
        //캐릭터의 position, 오브젝트의 이동속도, 오브젝트 간 거리 등을 사용할 때 쓰는 클래스
        //2D->Vector2(x,y)
        //3D->Vector3(x,y,z)

        
        //Quanternion: 게임 오브젝트의 3차원 방향을 저장
        //한 방향에서 다른 방향으로의 상대적인 회전 값
        //방향과 회전을 다 표현할 수 있는 클래스
        //180도 이상의 값을 표현할 수 없음
        //Quaternion.identity=회전값 0
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
