using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
//Shoot()에서 Rigidbody를 바로 요구하므로 해당 속성을 빼놓지 않도록 강제
//다른 사람이 보도록 쓰는 코드
public class ObjectShooter : MonoBehaviour
{
    // 발사 기능을 제공해주는 클래스(발사)
    // 충돌 시 오브젝트를 고정해주는 역할도 수행(발사 후 판정)
    GameObject objectGenerator;

    void Start()
    {
        objectGenerator = GameObject.Find("ObjectGenerator");
        //씬에서 해당 이름을 가진 게임오브젝트를 찾아 그 값을 얻어오는
        //GameObject.Find()기능

        ////==오브젝트 탐색 기능==
        //objectGenerator = GameObject.FindWithTag("Generator");
        ////Generator 태그를 가진 오브젝트 탐색
        ///
        //obj = FindObjectOfType<ObjectGenerator>();
        ////<>내 타입에 맞는 오브젝트 탐색

        //Find가 가장 쉬움
        //검색 범위가 많을 경우 불필요한 성능 저하 발생
        //이 때 Tag나 Type등으로 검색 범위 제한하여 탐색
        //scene에 해당 값이 없으면 null

    }

    /// <summary>
    /// 물체를 발사하는 함수
    /// </summary>
    /// <param name="direction">물체의 발사 방향</param>
    public void Shoot(Vector3 direction)
    {
        GetComponent<Rigidbody>().AddForce(direction);
        //objectGenerator 없이 GetComponent를 사용시 
        //게임 오브젝트가 직접 GetComponent를 사용

    }
    /// <summary>
    /// 충돌 시 오브젝트를 고정하는 함수
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true;       //충돌과 동시에 좌표 고정
        GetComponentInChildren<ParticleSystem>().Play();    
        //자식으로 등록된 파티클시스템 가동

        //지형과 충돌 시 1초후 파괴
        if (collision.gameObject.tag == "terrain")
        {
            Destroy(gameObject, 1.0f);
        }
        if(collision.gameObject.tag == "target")
        {
            objectGenerator.GetComponent<ObjectGenerator>().ScorePlus(10);
            Debug.Log("맞았습니다!");
        }
        if(collision.gameObject.tag == "bamsongi")
        {
            Destroy(gameObject,1f);
            Destroy(collision.gameObject,1f);

        }

    }


}
