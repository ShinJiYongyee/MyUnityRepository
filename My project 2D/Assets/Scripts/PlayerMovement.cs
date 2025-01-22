using System;
using UnityEngine;
//Rigidbody�� �̿��� �÷��̾� �̵� �ڵ�

//Ư�� ������Ʈ�� �䱸�ϴ� �Ӽ�, �ʼ����� ������Ʈ�� ������ ����
//������Ʈ�� ���� ������Ʈ�� ����� �ڵ����� ���� ����
[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //�ӵ� ����
    public int a = 10;              //���� �ʱ�ȭ
    public float speed = 2.0f;      //float �ڷ���(�Ҽ��� �Ʒ� 6�ڸ�)�Ҽ����� ���� ���� �������� f��ȣ ���
    public double jump_force = 3.5; //double �ڷ���(�Ҽ��� �Ʒ� 15�ڸ�)�� �Ǽ��� ǥ���ص� f�� ������� ����

    public bool isJump= false;

    private Rigidbody2D rigid;      //���� �ܺο��� �缳���� �ʿ䰡 �����Ƿ� private ����

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();    
        //GetComponent<T> ���׸� ����
        //�ش� ������Ʈ�� ���� ������ ���
        //<T>�� ������Ʈ�� ���¸� �ۼ�
        //������Ʈ�� ���°� �ٸ��ٸ� ���� �߻�
        //�ش� �����Ͱ� �������� ���� ����� null ��ȯ(null pointer error)
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
        //����ڰ� spaceŰ�� �Է��Ѵٸ�
        if (Input.GetKeyDown(KeyCode.Space))
        {

            //!�� ������ �ݴ�� ó��
            //jump���°� �ƴ� ��� jump���·� ����
            if (!isJump)
            {
                isJump = true;
                //���� �ִ� ����� �� ������ �ִ�
                //�� ���� ���� �ְų�
                //���� �ð��� ���� õõ�� �ְų�
                rigid.AddForce(Vector3.up * (float)jump_force, ForceMode2D.Impulse);
                //type casting
                //casting�� ������ ���������� ���(int->float)
            }
    

        }

    }

    //���� �ٸ� ��Ұ� �浹�� �� ���� ����=�÷��̾ �� ���� ���� �� jump Ȱ��ȭ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�浹ü�� ���� ������Ʈ ���̾ 7�� ���� ��
        //�±��� ��� �̸�����, ���̾��� ��� ���̾� �ѹ��� ����
        if (collision.gameObject.layer == 7)
        {
            isJump = false;
        }
        Debug.Log("���� ��ҽ��ϴ�.");

        if(collision.gameObject.layer == 6)
        {
            Debug.Log("���� �ε������ϴ�!!");
        }
        if (collision.gameObject.layer == 9)
        {
            Debug.Log("õ�忡 �Ӹ��� �ε������ϴ�!!");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("����!!!");
        }
    }
    private void Move()
    {
        float x = Input.GetAxisRaw ("Horizontal");
        float y = Input.GetAxisRaw ("Vertical");
        //GetAxisRaw("Ű �̸�")�� Input Manager�� Ű�� ��� Ŭ���� ���� -1, 0, 1�� ���� �޾� �� �̵�
        //Horizontal: ���� �̵�, a/d Ű �Ǵ� Ű���� �� �� ȭ��ǥ
        //Vertical: ���� �̵�, w/s Ű �Ǵ� Ű���� �� �Ʒ� ȭ��ǥ

        //�� �ڵ带 ���� ������ ���� ���
        Vector3 velocity= new Vector3 (x,y,0)*speed*Time.deltaTime;
        //�ӵ�=����*�ӷ�*��ŸŸ��(������ �� ���� ���͹� �г�Ƽ)
        transform.position += velocity;
    }
}
