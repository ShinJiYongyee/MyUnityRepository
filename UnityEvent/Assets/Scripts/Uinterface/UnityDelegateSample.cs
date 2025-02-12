using UnityEngine;
using System;

public class UnityDelegateSamples : MonoBehaviour
{
    //Action -> 반환 타입이 없는(void) 델리게이트
    //1)매개 변수가 따로 없는(void 만 등록 가능한) 대리자
    Action action;

    //2)매개변수가 있는 대리자
    Action<int> action2;
    Action<string, int> action3;

    //3)Func -> 반환 타입이 있는 대리자
    Func<int> func01;

    //4)매개 변수가 있는 대리자
    Func<int, float, string> func02;     //매개변수 (int, float), 반환형 string

    void Start()
    {
        //1)
        action = Sample;
        //action = Sample1;   //매개 변수가 있을 경우 등록 불가
        //action = Sample2;   //반환 타입이 있을 경우 등록 불가

        //2)
        //action2 = Sample;   //반환 타입이 없을 경우 등록 불가
        action2 = Sample1;

        //3) 대리자의 기능을 바로 구현해 사용하는 경우
        func01 = () => 10;          //int Test(){ return 10; } 와 기능이 동일한 함수
        Func<int> test = () => 25;

        //4) 매개변수가 존재하는 경우
        func02 = (a, b) => "a" + "b";

        // 여러 개의 식이 필요한 경우
        func02 = (a, b) =>
        {
            if (a > b)
            {
                a = (int)b;
            }
            return "a+b";
        };
    }
    public void Sample() { }
    public void Sample1(int a) { }
    public int Sample2() { return 0; }
}
