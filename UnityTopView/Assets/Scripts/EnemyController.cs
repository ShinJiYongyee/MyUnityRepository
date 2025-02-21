using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int hp = 3;
    public float speed = 0.5f;
    public float pattern_distance = 4.0f;   //탐지 범위
    public List<string> anime_list = new List<string>
    {
        "EnemyIdle","EnemyDown","EnemyUp","EnemyLeft","EnemyRight","EnemyDead"
    };
    string current = "";
    string previous = "";
    float h, v;     //가로축과 세로축의 값
    Rigidbody2D rbody;  //연결할 컴포넌트
    bool isActive = false;
    public int arrangeId = 0;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        //플레이어 존재시
        if (player != null)
        {
            //몬스터 활성화시
            if (isActive)
            {
                float dx = player.transform.position.x - transform.position.x;
                float dy = player.transform.position.y - transform.position.y;
                float radian = Mathf.Atan2(dy, dx);
                float degree = radian * Mathf.Rad2Deg;

                if (degree > -45.0f && degree <= 45.0f)
                {
                    current = anime_list[4];    //right
                }
                else if (degree > 45.0f && degree <= 135.0f)
                {
                    current = anime_list[2];    //up
                }
                else if (degree > 135.0f && degree <= -45.0f)
                {
                    current = anime_list[1];    //down
                }
                else
                {
                    current = anime_list[3];    //left
                }

                h = Mathf.Cos(radian) * speed;
                v = Mathf.Sin(radian) * speed;
            }
            //몬스터 비활성화시
            else
            {
                //플레이어와 거리를 측정
                float distance = Vector2.Distance(transform.position, player.transform.position);

                //탐지 범위 내 들어올 경우 몬스터 활성화
                if (distance < pattern_distance)
                {
                    isActive = true;
                }
            }
        }
        //플레이어가 없을 때 몬스터가 활성화 되어있다면 해당 몬스터 비활성화
        else if (isActive)
        {
            isActive =false;
            rbody.linearVelocity = Vector2.zero;
        }

    }

    private void FixedUpdate()
    {
        //활성화 상태이면서 체력이 남아있을 경우
        if (isActive && hp > 0)
        {
            //계산한 좌표로 이동
            rbody.linearVelocity = new Vector2(h, v);

            //애니메이션이 달라지면 변경후 플레이
            if(current != previous)
            {
                previous = current;
                var animator = GetComponent<Animator>();
                animator.Play(current);
            }
        }
    }

    //몬스터가 화살에 맞았을 때
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            hp--;
            if (hp <= 0)
            {
                //더는 충돌이 발생하지 않음
                GetComponent<CircleCollider2D>().enabled = false;
                //움직이지 않음
                rbody.linearVelocity = new Vector2(0, 0);
                //죽음 애니메이션 연출
                var animator = GetComponent<Animator>();
                animator.Play(anime_list[5]);
                //오브젝트 파괴
                Destroy(gameObject, 0.5f);
            }
        }
    }
}
