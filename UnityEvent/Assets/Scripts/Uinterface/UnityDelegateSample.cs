using UnityEngine;
using System;

public class UnityDelegateSamples : MonoBehaviour
{
    //Action -> ��ȯ Ÿ���� ����(void) ��������Ʈ
    //1)�Ű� ������ ���� ����(void �� ��� ������) �븮��
    Action action;

    //2)�Ű������� �ִ� �븮��
    Action<int> action2;
    Action<string, int> action3;

    //3)Func -> ��ȯ Ÿ���� �ִ� �븮��
    Func<int> func01;

    //4)�Ű� ������ �ִ� �븮��
    Func<int, float, string> func02;     //�Ű����� (int, float), ��ȯ�� string

    void Start()
    {
        //1)
        action = Sample;
        //action = Sample1;   //�Ű� ������ ���� ��� ��� �Ұ�
        //action = Sample2;   //��ȯ Ÿ���� ���� ��� ��� �Ұ�

        //2)
        //action2 = Sample;   //��ȯ Ÿ���� ���� ��� ��� �Ұ�
        action2 = Sample1;

        //3) �븮���� ����� �ٷ� ������ ����ϴ� ���
        func01 = () => 10;          //int Test(){ return 10; } �� ����� ������ �Լ�
        Func<int> test = () => 25;

        //4) �Ű������� �����ϴ� ���
        func02 = (a, b) => "a" + "b";

        // ���� ���� ���� �ʿ��� ���
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
