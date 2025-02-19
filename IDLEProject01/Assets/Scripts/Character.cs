using UnityEngine;

[RequireComponent(typeof(Animator))]    //animator 컴포넌트를 강제하는 설정
public class Character : MonoBehaviour
{
    Animator animator;
    public double HP;
    public double ATK;
    public float attack_speed;
    protected float attack_range = 2.0f;   //공격 거리(사거리/비거리)
    protected float target_range = 5.0f;   //현재 타겟과의 거리
    protected virtual void Start()
    {
        //animator 컴포넌트 받아오기
        animator = GetComponent<Animator>();
    }

    protected void SetMotionChange(string motion_name, bool param)
    {
        animator.SetBool(motion_name, param);
    }

    /// <summary>
    /// 애니메이션 이벤트에 의해 호출될 함수
    /// </summary>
    protected virtual void Thrown()
    {
        Debug.Log("발사");
    }

    protected Transform target; //현재 타겟의 위치

    /// <summary>
    /// 타겟을 찾는 기능, 타겟의 위치를 전달받아 작업
    /// </summary>
    protected void TargetSearch<T>(T[] targets) where T : Component
    {
        var units = targets;        //전달받은 값을 통해 할당
        Transform closest = null;   //가장 가까운 대상
        float max_distance = target_range;  //최대 거리 == 타겟과의 거리

        //타켓들 전체를 대상으로 거리 체크
        foreach (var unit in units)
        {
            //상대와의 거리 확인
            float distance = Vector3.Distance(transform.position, unit.transform.position);

            //현재 타겟의 거리보다 작으면 가장 가까운 값
            if (distance < max_distance)
            {
                closest = unit.transform;
                max_distance = distance;
            }
        }
        //타겟 적용
        target = closest;

        //현재 타겟을 응시
        if(target != null)
        {
            transform.LookAt(target.position);  
        }
    }
}
