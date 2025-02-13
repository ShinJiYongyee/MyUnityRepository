using UnityEngine.UI;
using UnityEngine;

public class BasicCode : MonoBehaviour
{
    //����Ƽ���� ���� Ŭ����
    //1. MonoBehavior�� ����Ǿ� �ִ� Ŭ����: ����Ƽ ���� ���� ������ �� �ִ� Ŭ����
    //2. �Ϲ� Ŭ����: ����Ƽ ������ Ư�� �����͸� ������ �� ����ϴ� Ŭ����
    //3. ScriptableObject�� ����� Ŭ����:
    //����Ƽ Assets���� ���ο� ��ũ��Ʈ�� �ּ����� ������ �� �ִ� Ŭ����

    //1. ���� ������
    //���α׷����� ���ٿ� ���õ� ������ �� �� �ִ� Ű����
    //public: ����Ƽ �ν����Ϳ��� Ȯ�� ����
    //privare: ����Ƽ �ν����Ϳ��� Ȯ�� �Ұ�
    //[SerializeField] �Ӽ��� ���� �ʵ��� ��쿡�� �ν����Ϳ��� ������
    //[Serializable] �Ӽ��� ���� Ŭ������ ������ ����� ��� �ν����Ϳ��� ������

    public int number;
    private int count;      //�ν����Ϳ��� ���� ����
    [SerializeField] private bool able;
    public Text text;
    public GameObject cube;
    public SampleCode s;

    //���� ������ �� 1ȸ ����Ǵ� �ڵ�
    //�ַ� ���� ���� ������ ����
    void Start()
    {
        //cube�� ��ϵ� ������Ʈ�� �� ���ӿ�����Ʈ�� �Ҵ�
        cube = new GameObject();

        //Cube��� ���ӿ�����Ʈ���Լ� SampleCode�� ���� �޾ƿ���
        s = GameObject.Find("Cube").GetComponent<SampleCode>();

        //�Լ� ȣ��: �Լ���(����)
        //�Ű�����(parameter) : �Լ� ���� �� ȣ���ϴ� �����κ��� ���� ������ ǥ��
        //����(Argument) : �Լ��� ȣ���� �� �־��ִ� ��
        NumberFive();
        Debug.Log(number);
        SetNumber(10);
        Debug.Log(number);
        TextHello();

    }

    //�޼ҵ�: Ŭ���� ���ο��� ��������� �Լ�(���� ����� �����ϴ� ��ɹ� ����ü)
    //���������� ��ȯ�� �Լ���(�Ű�����){ ��ɹ�; }
    //�Լ��� �̸��� ����� �� �� �ֵ��� �����ؾ� �Ѵ�

    //1. void ������ �Լ�
    // ==> ������ ��ɸ� ������ش�
    public void NumberFive()
    {
        number = 5;
    }

    public void SetNumber(int value)
    {
        number = value;
    }

    public void TextHello()
    {
        text.text = "Hello";    //UI�� Text ������ �ٲ۴�
    }

    //2. void �̿��� �Լ�
    // ==> ������ ������ ������ ��ȯ���� ����� �����Ѵ�


}
