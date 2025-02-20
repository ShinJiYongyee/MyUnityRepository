using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool is_countdown = true;     //ī��Ʈ�ٿ� ���� ����
    public float game_time = 60;         //���� ������ ���� �ð�(�ִ� �ð�)
    public bool is_timeover = false;    //Ÿ�ӿ��� ����       
    public float display_time = 0;      //ȭ�鿡 ǥ���ϱ� ���� �ð�
    float times = 0;                    //���� �ð�

    void Start()
    {
        //ī��Ʈ �ٿ��� ���� ���̶��, ǥ��� �ð��� ���� �ð����� ����
        if (is_countdown)
        {
            display_time = game_time;
        }
    }

    void Update()
    {
        if(!is_timeover)
        {
            times += Time.deltaTime;

            if (is_countdown)
            {
                display_time = game_time - times;

                if(display_time <= 0.0f)
                {
                    display_time = 0.0f;
                    is_timeover=true;
                }
            }
            else
            {
                display_time = times;
                if (display_time >= game_time)
                {
                    display_time = game_time;
                    is_timeover = true;
                }
            }
        }
    }
}
