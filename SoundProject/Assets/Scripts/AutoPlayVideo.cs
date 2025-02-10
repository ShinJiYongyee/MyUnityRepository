using UnityEngine;
using UnityEngine.Video;

public class AutoPlayVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer; // VideoPlayer ����
    public Transform player; // Player�� Transform ����
    public float triggerDistance = 5f; // ��� ���� �Ÿ�

    private bool isPlaying = false; // ���� ��� ���� Ȯ��

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>(); // VideoPlayer ��������
        }

        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform; // Player �ڵ� Ž��
        }

        videoPlayer.Stop(); // �ʱ� ���¿��� ���� ����
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= triggerDistance && !isPlaying)
        {
            videoPlayer.Play(); // �÷��̾ ������ ���� ���
            isPlaying = true;
        }
        else if (distance > triggerDistance && isPlaying)
        {
            videoPlayer.Stop(); // �÷��̾ �־����� ����
            isPlaying = false;
        }
    }
}
