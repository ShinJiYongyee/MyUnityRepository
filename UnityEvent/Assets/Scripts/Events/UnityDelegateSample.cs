using System;
using UnityEngine;
using UnityEngine.UI;

class MeetEvent
{
    public delegate void MeetEventHandler(string message);
    //EventHandler가 delegate이므로 
    public event MeetEventHandler meetHandler;

    public void Meet()
    {
        meetHandler("만난 것도 인연");
    }

}
public class UnityDelegateSample : MonoBehaviour
{
    public Text messageUI;

    //필드로 쓸 때에는 타입이 명확해야 하므로 var 사용 불가
    MeetEvent meetEvent = new MeetEvent();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meetEvent.meetHandler += EventMessage;
    }

    //EventHandler와 동일한 매개변수의 함수 자동완성
    private void EventMessage(string message)
    {
        //Debug.Log(message);
        messageUI.text = message;
    }

    public void OnMeetButton()
    {
        meetEvent.Meet();
    }
}
