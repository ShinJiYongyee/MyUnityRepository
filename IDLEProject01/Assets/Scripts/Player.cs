using UnityEngine;

public class Player : Character
{
    Vector3 start_pos;
    Quaternion rotation;
    public float speed = 2.0f;

    protected override void Start()
    {
        //Character 클래스 시작
        base.Start();

        //시작 값 설정
        start_pos = transform.position;
        rotation = transform.rotation;
    }

    void Update()
    {
        //타겟이 없을 때 시작 지점으로 되돌아오는 코드
        if(target == null)
        {
            //가까운 타겟 조사
            //리스트명.ToArray()를 통해 list->array
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
            return; //작업 종료
        }

        float distance = Vector3.Distance (transform.position, target.position);    

        //타겟 범위 안에 있으면서 공격 범위보다 멀리 떨어진 경우 -> 타겟에게 다가간다
        if(distance <= target_range || distance > attack_range )
        {
            SetMotionChange("isMOVE", true);
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime*speed);
        }
        //공격 범위 안에 들어온 경우 -> 공격한다
        else if(distance <= attack_range)
        {
            SetMotionChange("isATTACK",true );
        }
    }
}
