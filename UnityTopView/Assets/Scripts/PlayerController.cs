using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static int hp = 3;
    public static string state; //플레이 상태
    bool inDamage = false;  //데이지를 받는 상태인자 확인

    public float speed = 3.0f;
    public List<string> anime_list = new List<string>
    {
        "PlayerDown", "PlayerUp", "PlayerLeft", "PlayerRight", "PlayerDead"
    };
    bool isMove = false;
    string current = "";
    string previous = "";
    float h, v;     //가로축과 세로축의 값
    public float z;
    Rigidbody2D rbody;  //연결할 컴포넌트
    Animator animator;
    void Start()
    {
        state = "Playing";
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        previous = anime_list[0];   //게임 시작시 아래를 보고 있도록
    }

    // Update is called once per frame
    void Update()
    {
        //플레이 상태가 아니거나, 데미지를 받는 중엔 로직 처리x
        if (state!="Playing" || inDamage)
        {
            return;
        }



        if (isMove == false)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");

            Vector2 from = transform.position;
            Vector2 to = new Vector2 (from.x+h, from.y+v);

            z = GetAngle(from, to);   //키 입력값을 통해 이동 각도를 계산할 함수

            //각도에 따라 방향과 애니메이션 설정
            //public List<string> anime_list = new List<string>
            //{
            //    "PlayerDown", "PlayerUp", "PlayerLeft", "PlayerRight", "PlayerDead"
            //};
            if (z >= -45 && z < 45)         //오른쪽
            {
                current = anime_list[3];
            }
            else if (z >= 45 && z <= 135)   //위쪽
            {
                current = anime_list[1];
            }
            else if (z >= -135 && z <= -45) //아래쪽
            {
                current = anime_list[0];
            }
            else                            //왼쪽
            {
                current = anime_list[2];
            }

            if(current!= previous)
            {
                previous = current;
                animator.Play(current);
            }
        }
    }

    private void FixedUpdate()
    {
        //플레이 상태가 아니거나, 데미지를 받는 중엔 로직 처리x
        if (state != "Playing" || inDamage)
        {
            return;
        }
        //데미지를 받는 경우
        if (inDamage)
        {
            float value = Mathf.Sin(Time.time * 50);

            if (value > 0)
            {
                //이미지 표시
                GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                //이미지 비활성화
                GetComponent<SpriteRenderer>().enabled = false; 
            }
            //데미지를 받는 동안 조작 불가
            return;
        }
        rbody.linearVelocity = new Vector2(h, v) * speed;
    }

    //플레이어에게 물리적인 충돌 발생시
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            GetDamage(collision.gameObject);
        }
    }

    //적으로부터 받는 데미지 공식
    private void GetDamage(GameObject enemy)
    {
        if(state == "Playing")
        {
            hp--;
            if (hp > 0)
            {
                //이동 정지
                rbody.linearVelocity = new Vector2(0, 0);

                Vector3 to = (transform.position - enemy.transform.position).normalized;

                //현 좌표에서 약 4칸정도 멀어지도록
                rbody.AddForce(new Vector2(to.x * 4, to.y * 4));

                inDamage = true;

                //데미지 판정 후 호출할 함수 처리
                Invoke("OnDamageExit", 0.25f);
            }
            else
            {
                //체력이 0이 되면 게임 오버
                GameOver();
            }
        }
    }

    private void OnDamageExit()
    {
        inDamage = false;   
        GetComponent<SpriteRenderer>().enabled = true;  //이미지 다시 켜기
    }
    private void GameOver()
    {
        state = "Gameover";
        GetComponent<CircleCollider2D>().enabled = false;
        rbody.linearVelocity = new Vector2(0, 0);
        rbody.gravityScale = 1.0f;
        rbody.AddForce(new Vector2(0,5), ForceMode2D.Impulse);
        GetComponent<Animator>().Play(anime_list[4]);
        Destroy(gameObject, 1.0f);
    }

    /// <summary>
    /// from(시점)에서 to(종점)까지 각도를 계산하는 함수
    /// </summary>
    private float GetAngle(Vector2 from, Vector2 to)
    {
        float angle;

        if(h!=0||v!=0)
        {
            //from과 to의 차이를 계산
            float dx = to.x - from.x;
            float dy = to.y - from.y;
            
            //아크 탄젠트 값 계산 -> 탄젠트의 역함수
            //y값과 x값(=시점과 종점의 좌표상 차이)을 입력받아 각의 수치(radian)를 반환
            //Atan -> x=0일 경우 계산 불가
            float radian = Mathf.Atan2(dy, dx);

            //각의 수치를 각으로 변환
            angle = radian*Mathf.Rad2Deg;   

        }
        else    //움직이지 않는다면
        {
            angle = z;
        }
        return angle;
    }
}
