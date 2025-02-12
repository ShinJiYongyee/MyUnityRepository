using System;
using UnityEngine;
class SpecialPortalEvent
{
    public event EventHandler Kill;
    int count = 0;
    public void OnKill()
    {
        CountPlus();   //킬 수 증가
        if(count == 5)
        {
            count = 0;
            Kill(this, EventArgs.Empty);//킬 수가 5를 넘기면 초기화하며 이벤트 발동
        }
        else
        {
            //이벤트 진행 출력
            Debug.Log($"킬 이벤트 진행중 [{count}/5]");
        }
    }
    public void CountPlus() => count++; //람다식 -> 가독성 개선
}
public class EventSample : MonoBehaviour
{
    //1. 이벤트 정의
    //이벤트명 변수명 = new 이벤트명();
    SpecialPortalEvent specialPortalEvent = new SpecialPortalEvent();
    void Start()
    {
        //2. 이벤트 핸들러에 이벤트 연결
        specialPortalEvent.Kill += new EventHandler(MonsterKill);
        for(int i = 0; i < 5; i++)
        {
            specialPortalEvent.OnKill();//3. 이벤트 진행을 위한 기능 진행
        }
    }
    //4. 이벤트 발생시 실행 코드
    private void MonsterKill(object sender, EventArgs e)
    {
        Debug.Log("포털이 열렸습니다");
    }
}
