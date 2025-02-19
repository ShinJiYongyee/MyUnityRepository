using UnityEngine;

public class Player : Character
{
    Vector3 start_pos;
    Quaternion rotation;
    public float speed = 2.0f;

    protected override void Start()
    {
        //Character Ŭ���� ����
        base.Start();

        //���� �� ����
        start_pos = transform.position;
        rotation = transform.rotation;
    }

    void Update()
    {
        //Ÿ���� ���� �� ���� �������� �ǵ��ƿ��� �ڵ�
        if(target == null)
        {
            //����� Ÿ�� ����
            //����Ʈ��.ToArray()�� ���� list->array
            TargetSearch(Spawner.monster_list.ToArray());

            float pos = Vector3.Distance(transform.position, start_pos);
            if(pos > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position,start_pos, Time.deltaTime*speed);
                transform.LookAt(start_pos);
                SetMotionChange("isMOVE", true);
            }
            else
            {
                transform.rotation = rotation;
                SetMotionChange("isMOVE", false);
            }
            return; //�۾� ����
        }

        float distance = Vector3.Distance (transform.position, target.position);    

        //Ÿ�� ���� �ȿ� �����鼭 ���� �������� �ָ� ������ ��� -> Ÿ�ٿ��� �ٰ�����
        if(distance <= target_range || distance > attack_range )
        {
            SetMotionChange("isMOVE", true);
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime*speed);
        }
        //���� ���� �ȿ� ���� ��� -> �����Ѵ�
        else if(distance <= attack_range)
        {
            SetMotionChange("isATTACK",true );
        }
    }
}
