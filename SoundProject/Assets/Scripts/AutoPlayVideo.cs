using UnityEngine;
using UnityEngine.Video;

public class AutoPlayVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer; // VideoPlayer 참조
    public Transform player; // Player의 Transform 참조
    public float triggerDistance = 5f; // 재생 시작 거리

    private bool isPlaying = false; // 비디오 재생 여부 확인

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>(); // VideoPlayer 가져오기
        }

        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform; // Player 자동 탐색
        }

        videoPlayer.Stop(); // 초기 상태에서 비디오 정지
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= triggerDistance && !isPlaying)
        {
            videoPlayer.Play(); // 플레이어가 가까이 오면 재생
            isPlaying = true;
        }
        else if (distance > triggerDistance && isPlaying)
        {
            videoPlayer.Stop(); // 플레이어가 멀어지면 정지
            isPlaying = false;
        }
    }
}
