using System;
using UnityEngine;
//Rigidbody를 이용한 플레이어 이동 코드

//특정 컴포넌트를 요구하는 속성, 필수적인 컴포넌트의 삭제를 방지
//컴포넌트가 없는 오브젝트에 연결시 자동으로 연결 진행
[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //속도 설정
    public int a = 10;              //변수 초기화
    public float speed = 2.0f;      //float 자료형(소수점 아래 6자리)소수점을 적을 때는 마지막에 f기호 사용
    public double jump_force = 3.5; //double 자료형(소수점 아래 15자리)은 실수를 표기해도 f를 사용하지 않음

    public bool isJump= false;

    private Rigidbody2D rigid;      //굳이 외부에서 재설정할 필요가 없으므로 private 선언

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();    
        //GetComponent<T> 제네릭 문법
        //해당 컴포넌트의 값을 얻어오는 기능
        //<T>에 컴포넌트의 형태를 작성
        //컴포넌트의 형태가 다르다면 오류 발생
        //해당 데이터가 존재하지 않을 경우라면 null 반환(null pointer error)
        //
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }



    private void Jump()
    {
        //사용자가 space키를 입력한다면
        if (Input.GetKeyDown(KeyCode.Space))
        {

            //!는 조건의 반대로 처리
            //jump상태가 아닐 경우 jump상태로 변경
            if (!isJump)
            {
                isJump = true;
                //힘을 주는 방법은 두 가지가 있다
                //한 번에 많이 주거나
                //오랜 시간에 걸쳐 천천히 주거나
                rigid.AddForce(Vector3.up * (float)jump_force, ForceMode2D.Impulse);
                //type casting
                //casting이 가능한 범위에서만 허용(int->float)
            }
    

        }

    }

    //서로 다른 요소가 충돌할 시 동작 설정=플레이어가 땅 위에 있을 때 jump 활성화
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //충돌체의 게임 오브젝트 레이어가 7과 같을 때
        //태그일 경우 이름으로, 레이어일 경우 레이어 넘버로 정의
        if (collision.gameObject.layer == 7)
        {
            isJump = false;
        }
        Debug.Log("땅을 밟았습니다.");

        if(collision.gameObject.layer == 6)
        {
            Debug.Log("벽에 부딪혔습니다!!");
        }
        if (collision.gameObject.layer == 9)
        {
            Debug.Log("천장에 머리를 부딪혔습니다!!");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("골인!!!");
        }
    }
    private void Move()
    {
        float x = Input.GetAxisRaw ("Horizontal");
        float y = Input.GetAxisRaw ("Vertical");
        //GetAxisRaw("키 이름")은 Input Manager의 키를 얻어 클릭에 따라 -1, 0, 1씩 값을 받아 축 이동
        //Horizontal: 수평 이동, a/d 키 또는 키보드 좌 우 화살표
        //Vertical: 수직 이동, w/s 키 또는 키보드 위 아래 화살표

        //위 코드를 통해 움직일 방향 계산
        Vector3 velocity= new Vector3 (x,y,0)*speed*Time.deltaTime;
        //속도=방향*속력*델타타임(프레임 당 동작 인터벌 패널티)
        transform.position += velocity;
    }
}
