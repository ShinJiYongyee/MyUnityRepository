using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextCount : MonoBehaviour
{
    //�ؽ�Ʈ�� ī��Ʈ�� ����ϴ� ��� ����
    //ī��Ʈ�� �ʸ��� 1�� ����
    public Text countText;
    private int count;
    void Start()
    {
        StartCoroutine("CountPlus");
    }

    IEnumerator CountPlus()
    {
        while (true) 
        {
            count++;
            countText.text = count.ToString("N0");
            //�ؽ�Ʈ�� ���� ĭ �̸��� text
            //ToString�� �̿��� ���� ���ڿ��� ����
            //N0�� ���� 3�ڸ� �������� ,�� ǥ���ϴ� format ex)1,000
            yield return null;
            //���� �������� �Ѿ�� ��ũ��Ʈ
        }
    }
}
