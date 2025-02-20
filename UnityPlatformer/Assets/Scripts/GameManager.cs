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
    TimeController timeController;  //GameManager�� �⺻���� �������� ���

    void Start()
    {
        //timeController ���� �� ����
        timeController = GetComponent<TimeController>();

        if (timeController != null )
        {
            if(timeController.game_time == 0.0f)
            {
                time_bar.SetActive(false);  //�ð� ������ ���ٸ� ����
            }
        }
        //���� �ؽ�Ʈ�� �г� ����


        //1�� �� �ش� �Լ� ȣ��
        Invoke("InactiveImage", 1.0f);
        //�г� ��Ȱ��ȭ
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
            //�̹��� �� �г� Ȱ��ȭ
            main_image.SetActive(true);
            panel.SetActive(true);

            //���� Ŭ����� �ٽ� ���� ��ư ��Ȱ��ȭ
            restartButton.GetComponent<Button>().interactable = false;
            //���� �̹����� ���� Ŭ���� �̹����� ����
            main_image.GetComponent<Image>().sprite = game_clear_sprite;
            //�÷��̾� ��Ʈ�ѷ� ���¸� End�� ����
            PlayerController.state = "End";

            //Ÿ�� ��Ʈ�ѷ��� �����, ���� Ŭ����� ���ÿ� Ÿ�ӿ���
            if(timeController != null)
            {
                timeController.is_timeover = true;
            }
        }
        else if(PlayerController.state == "Gameover")
        {
            //�̹��� �� �г� Ȱ��ȭ
            main_image.SetActive(true);
            panel.SetActive(true);

            //���� Ŭ����� ���� é�� �̵� ��ư ��Ȱ��ȭ
            nextButton.GetComponent<Button>().interactable = false;
            //���� �̹����� ���� Ŭ���� �̹����� ����
            main_image.GetComponent<Image>().sprite = game_over_sprite;
            //�÷��̾� ��Ʈ�ѷ� ���¸� End�� ����
            PlayerController.state = "End";

            //Ÿ�� ��Ʈ�ѷ��� �����, ���� ������ ���ÿ� Ÿ�ӿ���
            if (timeController != null)
            {
                timeController.is_timeover = true;
            }

        }
        else if (PlayerController.state == "Playing")
        {
            //���� �÷��� �� �ʿ��� �κ��� �߰��� �ۼ�

            GameObject player = GameObject.FindGameObjectWithTag("Player");

            //player���� playercontroller�� �־����� �ŷ��ϴ� �ڵ�
            PlayerController playerController = player.GetComponent<PlayerController>();

            if(timeController != null)
            {
                if(timeController.game_time >0.0f)
                {
                    //���� ǥ��
                    int time = (int)timeController.display_time;
                    //UI �ð� ����
                    timeText.GetComponent<Text>().text = time.ToString();

                    if (time == 0)
                    {
                        //PlayerController�� GameOver�� public���� �����ؾ� ��
                        playerController.GameOver();
                    }
                }
            }
        }
    }
}
