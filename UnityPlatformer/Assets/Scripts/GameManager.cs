using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject main_image;
    public Sprite game_over_sprite;
    public Sprite game_clear_sprite;
    public GameObject panel;
    public GameObject restartButton;
    public GameObject nextButton;

    Image image;

    /* --- Time Controller --- */
    public GameObject time_bar;
    public GameObject timeText;
    TimeController timeController;  //GameManager에 기본으로 딸려오는 요소

    void Start()
    {
        //timeController 연결 및 설정
        timeController = GetComponent<TimeController>();

        if (timeController != null )
        {
            if(timeController.game_time == 0.0f)
            {
                time_bar.SetActive(false);  //시간 제한이 없다면 숨김
            }
        }
        //내용 텍스트와 패널 설정


        //1초 뒤 해당 함수 호출
        Invoke("InactiveImage", 1.0f);
        //패널 비활성화
        panel.SetActive(false);
    }

    void InactiveImage()
    {
        main_image.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.state == "Gameclear")
        {
            //이미지 및 패널 활성화
            main_image.SetActive(true);
            panel.SetActive(true);

            //게임 클리어시 다시 시작 버튼 비활성화
            restartButton.GetComponent<Button>().interactable = false;
            //메인 이미지를 게임 클리어 이미지로 변경
            main_image.GetComponent<Image>().sprite = game_clear_sprite;
            //플레이어 컨트롤러 상태를 End로 변경
            PlayerController.state = "End";

            //타임 컨트롤러가 존재시, 게임 클리어와 동시에 타임오버
            if(timeController != null)
            {
                timeController.is_timeover = true;
            }
        }
        else if(PlayerController.state == "Gameover")
        {
            //이미지 및 패널 활성화
            main_image.SetActive(true);
            panel.SetActive(true);

            //게임 클리어시 다음 챕터 이동 버튼 비활성화
            nextButton.GetComponent<Button>().interactable = false;
            //메인 이미지를 게임 클리어 이미지로 변경
            main_image.GetComponent<Image>().sprite = game_over_sprite;
            //플레이어 컨트롤러 상태를 End로 변경
            PlayerController.state = "End";

            //타임 컨트롤러가 존재시, 게임 오버와 동시에 타임오버
            if (timeController != null)
            {
                timeController.is_timeover = true;
            }

        }
        else if (PlayerController.state == "Playing")
        {
            //게임 플레이 중 필요한 부분을 추가로 작성

            GameObject player = GameObject.FindGameObjectWithTag("Player");

            //player에게 playercontroller를 넣었음을 신뢰하는 코드
            PlayerController playerController = player.GetComponent<PlayerController>();

            if(timeController != null)
            {
                if(timeController.game_time >0.0f)
                {
                    //정수 표기
                    int time = (int)timeController.display_time;
                    //UI 시간 갱신
                    timeText.GetComponent<Text>().text = time.ToString();

                    if (time == 0)
                    {
                        //PlayerController의 GameOver를 public으로 설정해야 함
                        playerController.GameOver();
                    }
                }
            }
        }
    }
}
