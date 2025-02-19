using System;
using UnityEngine;

public class Monster :  Character
{
    public float monster_speed;
    public float min_distance = 1.5f;   //접근을 중지할 거리(최소 간격)

    protected override void Start()
    {
        base.Start();   //부모 Character의 Start()를 실행
    }
    //Action 테스트
    public void MonsterSample()
    {
        Debug.Log("몬스터가 생성되었습니다");
    }
    void Update()
    {
        //시선을 플레이어 위치(영점)로 향하는 코드
        transform.LookAt(Vector3.zero);

        //간격 설정, 몬스터와 플레이어 위치 간 거리
        float target_distance=Vector3.Distance(transform.position, Vector3.zero);

        if(target_distance <= min_distance)     //간격만큼 가까워지면 이동 중지
        {
            SetMotionChange("isMOVE", false);
        }
        else                                    //가까워질 때까지 이동
        {
            //플레이어 위치로 몬스터 속도*프레임 보정값만큼의 속도로 움직이는 코드
            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime*monster_speed);

            //isMOVE 조건을 true로 바꾸는 함수
            SetMotionChange("isMOVE",true);
        }


    }


}
