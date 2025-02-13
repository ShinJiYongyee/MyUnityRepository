using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructureSample : MonoBehaviour
{
    //string ������ ���� ������ �� �ִ� ť
    public Queue<string> stringQueue = new Queue<string>();

    private void Start()
    {
        //1) ������ �߰�
        stringQueue.Enqueue("���� �����ּ���");
        stringQueue.Enqueue("���� ������?");
        stringQueue.Enqueue("�ٳ��� ���� 20���� �ʿ��ؿ�");
        stringQueue.Enqueue("���͵帮�ڽ��ϴ�");
        stringQueue.Enqueue("�����մϴ�");

        //2) ù ��° ������ ��ȸ
        foreach (string dialog in stringQueue)
        {
            Debug.Log(stringQueue.Peek());     //ť�� ù ��° �� ��ȯ
        }

        //3) ť�� ������ ����
        
        Debug.Log(stringQueue.Dequeue());  //ť�� ù ��° �� ��ȯ�� ���ÿ� ����
        Debug.Log(stringQueue.Dequeue());  //ť�� ù ��° �� ��ȯ�� ���ÿ� ����
        Debug.Log(stringQueue.Dequeue());  //ť�� ù ��° �� ��ȯ�� ���ÿ� ����
        Debug.Log(stringQueue.Dequeue());  //ť�� ù ��° �� ��ȯ�� ���ÿ� ����
        Debug.Log(stringQueue.Dequeue());  //ť�� ù ��° �� ��ȯ�� ���ÿ� ����

    }
}
