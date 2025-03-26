using UnityEngine;
using UnityEngine.Rendering;

public class DoorLeafManager : MonoBehaviour
{
    public bool isOpened = false;
    private Animator animatior;
    public bool LastOpenedForward {  get; private set; } //문이 정방향으로 열려있는지 추적

    private void Start()
    {
        animatior = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    public bool IsPlayerInfront(Transform player)
    {
        //플레이어와 문 사이의 벡터
        Vector3 toPlayer = (player.position - transform.position).normalized;
        //문 방향과 플레이어 방향을 내적으로 비교
        float dotProduct = Vector3.Dot(transform.forward, toPlayer);
        //dotProduct > 0 일 시 플레이어가 문 앞에 있음
        return dotProduct > 0;
    }

    public bool Open(Transform player)
    {
        if(!isOpened)
        {
            //문이 열린 상태로 설정
            isOpened = true;

            if(IsPlayerInfront(player)) //플레이어가 문 앞에 있으면
            {
                //정방향 개방
                animatior.SetTrigger("OpenForward");
                LastOpenedForward = true;
            }
            else //플레이어가 문 뒤에 있으면
            {
                //역방향 개방
                animatior.SetTrigger("OpenBackward");
                LastOpenedForward = false;
            }
            return true;
        }
        return false;
    }

    public void CloseForward(Transform player)
    {
        if(isOpened)
        {
            isOpened = false;
            animatior.SetTrigger("CloseForward");
        }
    }

    public void CloseBackward(Transform player)
    {
        if (isOpened)
        {
            isOpened = false;
            animatior.SetTrigger("CloseBackward");
        }
    }
}