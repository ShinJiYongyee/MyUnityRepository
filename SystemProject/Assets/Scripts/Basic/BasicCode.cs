using UnityEngine.UI;
using UnityEngine;

public class BasicCode : MonoBehaviour
{
    //유니티에서 만들 클래스
    //1. MonoBehavior가 연결되어 있는 클래스: 유니티 씬에 직접 연결할 수 있는 클래스
    //2. 일반 클래스: 유니티 내에서 특정 데이터를 설계할 때 사용하는 클래스
    //3. ScriptableObject가 연결된 클래스:
    //유니티 Assets폴더 내부에 스크립트를 애셋으로 저장할 수 있는 클래스

    //1. 접근 제한자
    //프로그램에서 접근에 관련된 설정을 할 수 있는 키워드
    //public: 유니티 인스펙터에서 확인 가능
    //privare: 유니티 인스펙터에서 확인 불가
    //[SerializeField] 속성이 붙은 필드의 경우에는 인스펙터에서 공개됨
    //[Serializable] 속성이 붙은 클래스를 변수로 사용할 경우 인스펙터에서 공개됨

    public int number;
    private int count;      //인스펙터에서 뜨지 않음
    [SerializeField] private bool able;
    public Text text;
    public GameObject cube;
    public SampleCode s;

    //씬을 시작할 때 1회 실행되는 코드
    //주로 값에 대한 설정을 진행
    void Start()
    {
        //cube에 등록된 오브젝트를 새 게임오브젝트에 할당
        cube = new GameObject();

        //Cube라는 게임오브젝트에게서 SampleCode의 값을 받아오기
        s = GameObject.Find("Cube").GetComponent<SampleCode>();

        //함수 호출: 함수명(인자)
        //매개변수(parameter) : 함수 설계 시 호출하는 쪽으로부터 받을 데이터 표현
        //인자(Argument) : 함수를 호출할 때 넣어주는 값
        NumberFive();
        Debug.Log(number);
        SetNumber(10);
        Debug.Log(number);
        TextHello();

    }

    //메소드: 클래스 내부에서 만들어지는 함수(단일 기능을 수행하는 명령문 집합체)
    //접근제한자 반환형 함수명(매개변수){ 명령문; }
    //함수의 이름은 기능을 알 수 있도록 설계해야 한다

    //1. void 형태의 함수
    // ==> 실행할 기능만 만들어준다
    public void NumberFive()
    {
        number = 5;
    }

    public void SetNumber(int value)
    {
        number = value;
    }

    public void TextHello()
    {
        text.text = "Hello";    //UI의 Text 내용을 바꾼다
    }

    //2. void 이외의 함수
    // ==> 실행이 끝나고 전달할 반환값을 고려해 설계한다


}
