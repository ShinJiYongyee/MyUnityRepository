using System;
using UnityEngine;

[Flags]
public enum DAY
{
   None = 0,
   �� = 1 << 0,
   �� = 1 << 1,  //2
   ȭ = 1 << 2,  //4
   �� = 1 << 3,  //8
   �� = 1 << 4,  //16
   �� = 1 << 5,  //32
   �� = 1 << 6,  //64


}
public enum JOB
{
    ������, ��������
}
public class DataExample : MonoBehaviour
{
    public string[] schedules;  //�迭, ���� �������� ����
    public DAY workDay; //��� ���Ͽ� � ���� �ϴ��� ����
    public JOB job;

    private void Start()
    {
        //������ ��ü�� ���� ���
        for (int i = 0; i < schedules.Length; i++)
        {
            Debug.Log(schedules[i]);
        }
        Debug.Log(workDay);
        Debug.Log(job);
    }
}
