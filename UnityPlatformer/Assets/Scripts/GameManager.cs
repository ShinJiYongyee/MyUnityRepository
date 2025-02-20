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

    void Start()
    {
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
        }
        else if (PlayerController.state == "Playing")
        {
            //게임 플레이 중 필요한 부분을 추가로 작성
        }
    }
}
