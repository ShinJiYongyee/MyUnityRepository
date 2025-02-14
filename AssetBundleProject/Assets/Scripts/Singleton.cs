using UnityEngine;

public class Tester: MonoBehaviour
{
    int point = 0;

    private void Start()
    {
        //��ü�� ���� ������ ����ϴ� ���
        point = Singleton.Instance.point;   //�̱��濡 �ִ� ������Ƽ
        Singleton.Instance.PointPlus();     //�޼ҵ带 ���� Ŭ���� ���� ��ü�� ����

        //�̱��� ������ ������ ������ �ʿ� ���� �ٷ� ����� ����� �� �ִ�
        //�׷��� �̱��� �������� ������ �ν��Ͻ��� �ʹ� ���� �����͸� ������ ���
        //������ ��ư� �׽�Ʈ�� ��ٷο�����
    }
}
public class Singleton : MonoBehaviour
{
    //1. �ν��Ͻ��̸鼭 ���� ���� �������� ������ �� �ְ�(static) ����
    private static Singleton _instance;

    //2. Ŭ���� ���ο� ǥ���� �� ����
    public int point;

    public void PointPlus()
    {
        point++;
    }
    public void ViewPoint()
    {
        Debug.Log("���� ����Ʈ" + point);
    }
    //�޼ҵ带 ���ؼ� ����
    public Singleton GetInstance()
    {
        //���� ���� ����ִٸ�
        if (_instance == null)
        {
            //���Ӱ� �Ҵ�
            _instance = new Singleton();
        }
        //�Ϲ����� ����� ������ �ν��Ͻ��� ��ȯ
        return _instance;
    }

    //������Ƽ�� ���� ����
    public static Singleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }
    }
}
