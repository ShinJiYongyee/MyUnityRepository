using UnityEngine;
/// <summary>
/// 큐브를 회전시키는 클래스(컴포넌트)
/// </summary>
public class CubeLotate : MonoBehaviour
{
    #region 필기 내용
    //자료형(type): 프로그램이 데이터를 판단하는 기준
    //자주 사용되는 자료형: int, float, bool, string

    //변수(variable): 값을 저장할 수 있는 공간
    //특정 값 하나를 저장하기 위해 이름을 붙인 저장공간
    //만드는 방법
    //자료형 변수명(= 값);

    #endregion

    #region 변수
    
    public float x;     //inspector에서 조절하는 값
    public float y;
    public float z;
    private int sample; //내부에서 계산되므로 외부에서 건드릴 필요가 없는 데이터
    #endregion

    #region 함수
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sample = 10;    //변수가 비공개일 경우 대부분 코드 내부에서 설정
        Debug.Log(sample);  
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(x*Time.deltaTime,y*Time.deltaTime,z*Time.deltaTime));
        //deltaTime: 전 프레임이 완료되기까지 걸리는 시간
    }
    #endregion
}
