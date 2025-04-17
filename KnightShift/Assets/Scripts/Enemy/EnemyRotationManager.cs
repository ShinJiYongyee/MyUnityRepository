using UnityEngine;

public class EnemyRotationManager : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        //�÷��̾ ��ü�� �����ʿ� ������ �������� �ٶ󺻴�
        if((player.transform.position.x - transform.position.x) > 0)
        {
            //transform.Rotate(0, 180, 0);  //��� ȸ��, ��ü ���� ȸ���� ����
            //�÷��̾���� ������� �� ������ �ſ� ������ ���ڸ� ��ȸ

            transform.rotation = Quaternion.Euler(0, 180, 0); //���� ȸ��, ���� ���� ȸ���� ����, ���� ����
        }
        else
        {
            //transform.Rotate(0, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
