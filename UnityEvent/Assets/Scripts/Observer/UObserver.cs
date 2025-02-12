using UnityEngine;

/// <summary>
/// 옵저버에 대한 관리, 활용을 진행하기 위한 인터페이스
/// </summary>
public interface ISubject 
{ 
    //옵저버 등록
    void Add(UObserver observer);
    //옵저버 제거
    void Remove(UObserver observer);
    //갱신
    void Notify();
}

//abstract class: 메소드에 대한 선언을 진행할 수 있는 클래스
//인터페이스와 유사
//옵저버 구현에 사용
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
