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
        }
        else if (PlayerController.state == "Playing")
        {
            //���� �÷��� �� �ʿ��� �κ��� �߰��� �ۼ�
        }
    }
}
