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

    //�ִϸ��̼� �̸�
    public enum ANIME_STATE
    {
        PlayerIDLE,
        PlayerClear,
        PlayerGameOver,
        PlayerRun,
        PlayerJump
    }

    Animator animator;
    string current = "";     //���� �������� �ִϸ��̼�
    string previous = "";    //������ �����ϴ� �ִϸ��̼�

    public static string state = "Playing"; //���� ����

    void Start()
    {
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        state = "Playing";  //������� �� ���¸� �缳��
    }

    void Update()
    {
        //�÷��� ���� �ƴ� �� ���� ����
        if(state != "Playing")
        {
            return;
        }

        axisH = Input.GetAxisRaw("Horizontal"); //ĭ ����(Raw) ���� �̵�(Horizontal)

        if (axisH > 0.0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            transform.localScale = new Vector2(-1, 1);   //���⿡ �������� ������ �¿� ����
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    //���� ����� �ٷ�� �Լ�
    //���� �ð� ����(�ʴ� 50ȸ)���� ȣ��, ���������� �ùķ��̼� ����
    private void FixedUpdate()
    {
        //�÷��� ���� �ƴ� �� ���� ����
        if (state != "Playing")
        {
            return;
        }

        //������ �� ���� �����ϴ� ������ ���� GameObject�� �����ϴ����� ������ true/false�� ��ȯ
        //up�� Vector���� (0,1,0)
        //�÷��̾��� ���� pivot�� bottom
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        //���� ���� �ְų� �ӵ��� 0�� �ƴ� ���
        if (onGround || axisH != 0)
        {
            rigid2D.linearVelocity = new Vector2(speed * axisH, rigid2D.linearVelocityY);
        }

        //���� ���� �ִ� ���¿��� ���� Ű�� ���� ��Ȳ
        if (onGround && goJump)
        {
            //�÷��̾��� ���� ��ġ��ŭ ���� ����
            Vector2 jumpPW = new Vector2(0, jump);
            //�ش� ��ġ�� ���� ���Ѵ�(����)
            rigid2D.AddForce(jumpPW, ForceMode2D.Impulse);
            goJump = false;
        }

        //�ִϸ��̼� ��ȯ

        if (onGround)   //�� ���� ���� ��
        {
            if(axisH == 0)  //�������� ����
            {
                //�ش� enum�� Ư�� ��(���� �̸�)�� ��������
                current = Enum.GetName(typeof(ANIME_STATE),0);
            }
            else    //������(�޸�)
            {
                current = Enum.GetName(typeof(ANIME_STATE), 3);
            }
        }
        else    //���߿� ���� ��
        {
            current = Enum.GetName(typeof(ANIME_STATE), 4);
        }

        //���� ����� ���� ��ǰ� �ٸ� ���(�ִϸ��̼� ����)
        if(current != previous)
        {
            previous = current;
            animator.Play(current);
        }
    }

    private void Jump()
    {
        goJump = true;  //�÷��� Ű�� �۾�
    }

    //Collider ��� Ʈ���� �浹
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

    //����(�÷��̾� ������)�� ���ߴ� �ڵ�
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
        GetComponent<CapsuleCollider2D>().enabled = false;  //�÷��̾��� Collider ��Ȱ��ȭ
        rigid2D.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);   //���� ������ ��¦ �پ� ������ ����
    }



}
