using UnityEngine;

public class Tester: MonoBehaviour
{
    int point = 0;

    private void Start()
    {
        //객체가 갖는 정보를 사용하는 방법
        point = Singleton.Instance.point;   //싱글톤에 있는 프로퍼티
        Singleton.Instance.PointPlus();     //메소드를 통해 클래스 내부 객체에 접근

        //싱글톤 패턴은 별도로 가져올 필요 없이 바로 기능을 사용할 수 있다
        //그러나 싱글톤 패턴으로 설계한 인스턴스가 너무 많은 데이터를 공유할 경우
        //수정이 어렵고 테스트가 까다로워진다
    }
}
public class Singleton : MonoBehaviour
{
    //1. 인스턴스이면서 선언 없이 전역으로 접근할 수 있게(static) 설계
    private static Singleton _instance;

    //2. 클래스 내부에 표현할 값 설계
    public int point;

    public void PointPlus()
    {
        point++;
    }
    public void ViewPoint()
    {
        Debug.Log("현재 포인트" + point);
    }
    //메소드를 통해서 전달
    public Singleton GetInstance()
    {
        //현재 값이 비어있다면
        if (_instance == null)
        {
            //새롭게 할당
            _instance = new Singleton();
        }
        //일반적인 경우라면 현재의 인스턴스를 반환
        return _instance;
    }

    //프로퍼티로 설계 가능
    public static Singleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }
    }
}
