using UnityEngine;
using UnityEngine.Rendering;

public class DoorLeafManager : MonoBehaviour
{
    public bool isOpened = false;
    private Animator animatior;
    public bool LastOpenedForward {  get; private set; } //���� ���������� �����ִ��� ����

    private void Start()
    {
        animatior = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    public bool IsPlayerInfront(Transform player)
    {
        //�÷��̾�� �� ������ ����
        Vector3 toPlayer = (player.position - transform.position).normalized;
        //�� ����� �÷��̾� ������ �������� ��
        float dotProduct = Vector3.Dot(transform.forward, toPlayer);
        //dotProduct > 0 �� �� �÷��̾ �� �տ� ����
        return dotProduct > 0;
    }

    public bool Open(Transform player)
    {
        if(!isOpened)
        {
            //���� ���� ���·� ����
            isOpened = true;

            if(IsPlayerInfront(player)) //�÷��̾ �� �տ� ������
            {
                //������ ����
                animatior.SetTrigger("OpenForward");
                LastOpenedForward = true;
            }
            else //�÷��̾ �� �ڿ� ������
            {
                //������ ����
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