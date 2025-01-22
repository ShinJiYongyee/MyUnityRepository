using System;
using UnityEngine;

[Flags]
public enum DAY
{
   None = 0,
   일 = 1 << 0,
   월 = 1 << 1,  //2
   화 = 1 << 2,  //4
   수 = 1 << 3,  //8
   목 = 1 << 4,  //16
   금 = 1 << 5,  //32
   토 = 1 << 6,  //64


}
public enum JOB
{
    직장인, 프리랜서
}
public class DataExample : MonoBehaviour
{
    public string[] schedules;  //배열, 같은 데이터의 묶음
    public DAY workDay; //어느 요일에 어떤 일을 하는지 저장
    public JOB job;

    private void Start()
    {
        //스케줄 전체의 내용 출력
        for (int i = 0; i < schedules.Length; i++)
        {
            Debug.Log(schedules[i]);
        }
        Debug.Log(workDay);
        Debug.Log(job);
    }
}
