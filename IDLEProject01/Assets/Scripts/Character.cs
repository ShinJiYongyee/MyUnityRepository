using UnityEngine;

[RequireComponent(typeof(Animator))]    //animator ������Ʈ�� �����ϴ� ����
public class Character : MonoBehaviour
{
    Animator animator;
    public double HP;
    public double ATK;
    public float attack_speed;
    protected float attack_range = 2.0f;   //���� �Ÿ�(��Ÿ�/��Ÿ�)
    protected float target_range = 5.0f;   //���� Ÿ�ٰ��� �Ÿ�
    protected virtual void Start()
    {
        //animator ������Ʈ �޾ƿ���
        animator = GetComponent<Animator>();
    }

    protected void SetMotionChange(string motion_name, bool param)
    {
        animator.SetBool(motion_name, param);
    }

    /// <summary>
    /// �ִϸ��̼� �̺�Ʈ�� ���� ȣ��� �Լ�
    /// </summary>
    protected virtual void Thrown()
    {
        Debug.Log("�߻�");
    }

    protected Transform target; //���� Ÿ���� ��ġ

    /// <summary>
    /// Ÿ���� ã�� ���, Ÿ���� ��ġ�� ���޹޾� �۾�
    /// </summary>
    protected void TargetSearch<T>(T[] targets) where T : Component
    {
        var units = targets;        //���޹��� ���� ���� �Ҵ�
        Transform closest = null;   //���� ����� ���
        float max_distance = target_range;  //�ִ� �Ÿ� == Ÿ�ٰ��� �Ÿ�

        //Ÿ�ϵ� ��ü�� ������� �Ÿ� üũ
        foreach (var unit in units)
        {
            //������ �Ÿ� Ȯ��
            float distance = Vector3.Distance(transform.position, unit.transform.position);

            //���� Ÿ���� �Ÿ����� ������ ���� ����� ��
            if (distance < max_distance)
            {
                closest = unit.transform;
                max_distance = distance;
            }
        }
        //Ÿ�� ����
        target = closest;

        //���� Ÿ���� ����
        if(target != null)
        {
            transform.LookAt(target.position);  
        }
    }
}
