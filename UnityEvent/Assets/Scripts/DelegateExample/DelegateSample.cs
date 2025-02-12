using UnityEngine;

public class DelegateSample : MonoBehaviour
{
    delegate void DelegateTest();
    void Start()
    {
        //��������Ʈ ���
        //��������Ʈ�� ������ = new ��������Ʈ��(�Լ���);
        DelegateTest dt = new DelegateTest(Attack);

        //�Լ�ó�� ȣ��
        dt();               //����

        //���� ����
        //������ = �Լ���;
        dt = Guard;
        dt();               //���
        //��������Ʈ ü��
        dt += Attack;
        dt += Attack;
        dt += Attack;
        dt += Attack;
        dt -= Guard;        //��� ����
        dt();               //���� 4��
    }
    void Attack()
    {
        Debug.Log("����!");
    }
    void Guard()
    {
        Debug.Log("���");
    }

}
