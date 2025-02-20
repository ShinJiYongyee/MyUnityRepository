using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    float axisH = 0.0f;
    public float speed = 3.0f;

    public float jump = 9.0f;
    public LayerMask groundLayer;

    bool onGround = false;
    bool goJump = false;

    //애니메이션 이름
    public enum ANIME_STATE
    {
        PlayerIDLE,
        PlayerClear,
        PlayerGameOver,
        PlayerRun,
        PlayerJump
    }

    Animator animator;
    string current = "";     //현재 진행중인 애니메이션
    string previous = "";    //기존에 진행하던 애니메이션

    public static string state = "Playing"; //현재 상태

    void Start()
    {
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        state = "Playing";  //재시작할 때 상태를 재설정
    }

    void Update()
    {
        //플레이 중이 아닐 시 게임 종료
        if(state != "Playing")
        {
            return;
        }

        axisH = Input.GetAxisRaw("Horizontal"); //칸 단위(Raw) 수평 이동(Horizontal)

        if (axisH > 0.0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            transform.localScale = new Vector2(-1, 1);   //방향에 음수값을 적용해 좌우 반전
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    //물리 계산을 다루는 함수
    //일정 시간 간격(초당 50회)으로 호출, 안정적으로 시뮬레이션 지속
    private void FixedUpdate()
    {
        //플레이 중이 아닐 시 게임 종료
        if (state != "Playing")
        {
            return;
        }

        //지정한 두 점을 연결하는 가상의 선에 GameObject가 접촉하는지를 조사해 true/false로 반환
        //up은 Vector기준 (0,1,0)
        //플레이어의 현재 pivot은 bottom
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        //지면 위에 있거나 속도가 0이 아닌 경우
        if (onGround || axisH != 0)
        {
            rigid2D.linearVelocity = new Vector2(speed * axisH, rigid2D.linearVelocityY);
        }

        //지면 위에 있는 상태에서 점프 키가 눌린 상황
        if (onGround && goJump)
        {
            //플레이어의 점프 수치만큼 벡터 설계
            Vector2 jumpPW = new Vector2(0, jump);
            //해당 위치로 힘을 가한다(점프)
            rigid2D.AddForce(jumpPW, ForceMode2D.Impulse);
            goJump = false;
        }

        //애니메이션 전환

        if (onGround)   //땅 위에 있을 때
        {
            if(axisH == 0)  //움직이지 않음
            {
                //해당 enum의 특정 값(값의 이름)을 가져오기
                current = Enum.GetName(typeof(ANIME_STATE),0);
            }
            else    //움직임(달림)
            {
                current = Enum.GetName(typeof(ANIME_STATE), 3);
            }
        }
        else    //공중에 있을 때
        {
            current = Enum.GetName(typeof(ANIME_STATE), 4);
        }

        //현재 모션이 이전 모션과 다를 경우(애니메이션 변경)
        if(current != previous)
        {
            previous = current;
            animator.Play(current);
        }
    }

    private void Jump()
    {
        goJump = true;  //플래그 키는 작업
    }

    //Collider 기반 트리거 충돌
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            Goal();
        }
        else
        {
            GameOver();
        }
    }

    //게임(플레이어 움직임)을 멈추는 코드
    private void GameStop()
    {
        rigid2D.linearVelocity = new Vector2(0, 0); 
    }

    private void Goal()
    {
        animator.Play(Enum.GetName(typeof(ANIME_STATE), 1));
        state = "Gameclear";
        GameStop();
    }

    public void GameOver()
    {
        animator.Play(Enum.GetName(typeof(ANIME_STATE), 2));
        state = "Gameover";
        GameStop();
        GetComponent<CapsuleCollider2D>().enabled = false;  //플레이어의 Collider 비활성화
        rigid2D.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);   //게임 오버시 살짝 뛰어 오르는 연출
    }



}
