using UnityEngine;

//해당 클래스로 만들 객체의 정보
class Unit
{
    public string unit_name;    //필드(클래스의 변수)

    //메소드(함수, 클래스로 생성한 객체의 기능)
    public static void UnitAction()
    {
        Debug.Log("유닛이 동작합니다");
    }
    public void Cry()
    {
        Debug.Log("유닛이 울부짖었습니다");
    }
}
public class CSharpScript : MonoBehaviour
{
    Unit unit;  //해당 클래스의 객체 생성

    private void Start()
    {
        unit.unit_name = "MiniGun"; //객체의 필드 접근

        unit.Cry(); //인스턴스의 메소드 사용

        Unit.UnitAction();  
        //static 필드/메소드는 생성된 객체를 호출하지 않고
        //클래스로부터 호출
    }
}
