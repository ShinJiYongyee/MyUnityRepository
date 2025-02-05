using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    // 오브젝트를 생성하는 스크립트
    // 마우스 버튼을 누를 때 카메라의 스크린 지점을 통해 물체의 방향을 설정
    // 물체를 방향에 맞춰 발사하는 기능을 호출

    public GameObject prefab;   //오브젝트 프리팹 등록
    public float power = 1000f; //던지는 힘
    GameObject scoreText;       //점수 표시 텍스트
    public int score = 0;       //매니저가 없는 상태에서 만드므로 점수를 별도로 설정

    void Update()
    {
        ///마우스 0번 버튼을 눌렀을 때
        ///0: LMB
        ///1: RMB
        ///2: MMB
        ///~~Down: 클릭시 1회
        ///~~Up: 버튼을 놓았을 때 1회
        ///~~: 누르는 동안 계속

        //좌클릭 1회시 구동되는 함수
        //1번 발사
        //bool을 반환
        if (Input.GetMouseButtonDown(0))
        {
            var thrown = Instantiate(prefab);
            //as GameObject는 Instantiate와 같이 사용하면
            //게임오브젝트로서 생성한다는 의미
            //그러나 비활성화됨->오늘날에는 별도로 사용하지 않음

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //카메라가 보는 방향으로 쏘는 가상의 빔을 벡터로 받아오는 코드
            //Ray: 발사되는 시점과 방향을 갖는 가상의 레이저(Vector3)
            //Ray2D: Vector2 형태의 Ray

            Vector3 direction = ray.direction;
            //ray의 방향을 벡터로서 받아옴

            thrown.GetComponent<ObjectShooter>().Shoot(direction.normalized*power);
            //ObjectShooter의 Shoot() 함수를 public으로 설정해야 구동
            //방향에 관계없이 같은 힘을 원하므로 normalized
        }
    }
    /// <summary>
    /// 점수 획득
    /// </summary>
    /// <param name="value"></param>
    public void Start()
    {
        scoreText = GameObject.Find("score");   //오브젝트 score를 찾아 가져오기
        SetScoreText();                         //최초 1회
    }
    public void ScorePlus(int value)
    {
        score += value;
        SetScoreText();     //최초 텍스트 설정
    }
    /// <summary>
    /// 점수 텍스트 설정
    /// 여러 번 사용할 기능이므로 함수화
    /// </summary>
    void SetScoreText()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text=$"점수: {score}";
    }
}
