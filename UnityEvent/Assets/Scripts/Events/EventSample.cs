using System;
using UnityEngine;
class SpecialPortalEvent
{
    public event EventHandler Kill;
    int count = 0;
    public void OnKill()
    {
        CountPlus();   //ų �� ����
        if(count == 5)
        {
            count = 0;
            Kill(this, EventArgs.Empty);//ų ���� 5�� �ѱ�� �ʱ�ȭ�ϸ� �̺�Ʈ �ߵ�
        }
        else
        {
            //�̺�Ʈ ���� ���
            Debug.Log($"ų �̺�Ʈ ������ [{count}/5]");
        }
    }
    public void CountPlus() => count++; //���ٽ� -> ������ ����
}
public class EventSample : MonoBehaviour
{
    //1. �̺�Ʈ ����
    //�̺�Ʈ�� ������ = new �̺�Ʈ��();
    SpecialPortalEvent specialPortalEvent = new SpecialPortalEvent();
    void Start()
    {
        //2. �̺�Ʈ �ڵ鷯�� �̺�Ʈ ����
        specialPortalEvent.Kill += new EventHandler(MonsterKill);
        for(int i = 0; i < 5; i++)
        {
            specialPortalEvent.OnKill();//3. �̺�Ʈ ������ ���� ��� ����
        }
    }
    //4. �̺�Ʈ �߻��� ���� �ڵ�
    private void MonsterKill(object sender, EventArgs e)
    {
        Debug.Log("������ ���Ƚ��ϴ�");
    }
}
