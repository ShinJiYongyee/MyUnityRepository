using System;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    public float speed = 12.0f;
    public float delay = 0.25f;
    public GameObject bowPrefab;
    public GameObject arrowPrefab;

    bool inAttack = false;  //공격 중인지 여부를 표시
    GameObject bowObject;

    void Start()
    {
        Vector3 pos = transform.position;
        bowObject = Instantiate(bowPrefab, pos, Quaternion.identity);
        bowObject.transform.SetParent(transform);   //활 오브젝트의 부모를 플레이어로 등록
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Attack();
        }

        //rotate bow, order
        float bowZ = -1;    //활이 플레이어보다 앞에 나온다
        var player_controller = GetComponent<PlayerController>();

        if (player_controller.z > 30 && player_controller.z < 150)
        {
            bowZ = 1;
        }
        //오일러 각(일반적인 각도 체계)->Quaternion(각 축을 한번에 계산)으로 변경
        //오일러 각을 이용해 회전 시 회전축이 겹칠 수 있음(짐벌 락)
        //Quaternion은 x,y,z,w(스칼라) 4가지 값을 이용
        //각도 기준 계산에 Quaternion 사용
        bowObject.transform.rotation = Quaternion.Euler(0, 0, player_controller.z);

        bowObject.transform.position = new Vector3(transform.position.x, transform.position.y, bowZ);


    }

    private void Attack()
    {
        //화살을 갖고 있으며 공격 상태가 아닐 경우
        if (ItemKeeper.hasArrows > 0 && inAttack == false)
        {
            ItemKeeper.hasArrows--;
            inAttack = true;

            var player_controller = GetComponent<PlayerController>();

            float z = player_controller.z;   //회전 각

            var rotation = Quaternion.Euler(0, 0, z);

            //계산한 회전각으로 오브젝트 생성
            var arrow_object = Instantiate(arrowPrefab, transform.position, rotation);

            //각을 지정해 발사
            float x = Mathf.Cos(z * Mathf.Deg2Rad);
            float y=Mathf.Sin(z * Mathf.Deg2Rad);
            Vector3 vector = new Vector3(x, y) * speed;
            var rbody = arrow_object.GetComponent<Rigidbody2D>();
            rbody.AddForce(vector, ForceMode2D.Impulse);

            //발사 딜레이만큼 지연, 공격 모드 해제
            Invoke("AttackChange", delay);


        }
    }

    public void AttackChange()
    {
        inAttack = false;
    }
}
