using UnityEngine;

/// <summary>
/// �������� ���� ����, Ȱ���� �����ϱ� ���� �������̽�
/// </summary>
public interface ISubject 
{ 
    //������ ���
    void Add(UObserver observer);
    //������ ����
    void Remove(UObserver observer);
    //����
    void Notify();
}

//abstract class: �޼ҵ忡 ���� ������ ������ �� �ִ� Ŭ����
//�������̽��� ����
//������ ������ ���
public abstract class UObserver 
{
    public abstract void OnNotify();
}

public class Observer1 : UObserver
{
    public override void OnNotify()
    {
        Debug.Log("UObserver action #1");
    }
}

public class Observer2 : UObserver
{
    public override void OnNotify()
    {
        Debug.Log("UObserver action #2");
    }
}
