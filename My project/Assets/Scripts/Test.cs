using UnityEngine;


/// <summary>
/// ó������ ���� ����Ƽ�� ��ũ��Ʈ
/// </summary>
public class Test : MonoBehaviour
//MonoBehaviour�� Ŭ������ �������� ���
//����Ƽ ���� �����ϴ� ������Ʈ�� ��ũ��Ʈ�� ������ �� �ְ� ����
//�߰������� ����Ƽ���� �������ִ� ����� ����� �� ���
{
    /// <summary>
    /// �����ϴ� ���
    /// </summary>
    /// <param name="damage">���ݷ�</param>
    void attack(int damage)
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hello, World!");    //�ܼ�â�� �޼��� �ۼ�
    }

    int count = 0;

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"{count} �¿� �ݺ� �ٱ�");
        count++;
    }
}
