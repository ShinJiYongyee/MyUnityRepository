using System;
using UnityEngine;

public class Monster :  Character
{
    public float monster_speed;
    public float min_distance = 1.5f;   //������ ������ �Ÿ�(�ּ� ����)

    protected override void Start()
    {
        base.Start();   //�θ� Character�� Start()�� ����
    }
    //Action �׽�Ʈ
    public void MonsterSample()
    {
        Debug.Log("���Ͱ� �����Ǿ����ϴ�");
    }
    void Update()
    {
        //�ü��� �÷��̾� ��ġ(����)�� ���ϴ� �ڵ�
        transform.LookAt(Vector3.zero);

        //���� ����, ���Ϳ� �÷��̾� ��ġ �� �Ÿ�
        float target_distance=Vector3.Distance(transform.position, Vector3.zero);

        if(target_distance <= min_distance)     //���ݸ�ŭ ��������� �̵� ����
        {
            SetMotionChange("isMOVE", false);
        }
        else                                    //������� ������ �̵�
        {
            //�÷��̾� ��ġ�� ���� �ӵ�*������ ��������ŭ�� �ӵ��� �����̴� �ڵ�
            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime*monster_speed);

            //isMOVE ������ true�� �ٲٴ� �Լ�
            SetMotionChange("isMOVE",true);
        }


    }


}
