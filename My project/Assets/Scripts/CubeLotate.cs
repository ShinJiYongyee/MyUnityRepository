using UnityEngine;
/// <summary>
/// ť�긦 ȸ����Ű�� Ŭ����(������Ʈ)
/// </summary>
public class CubeLotate : MonoBehaviour
{
    #region �ʱ� ����
    //�ڷ���(type): ���α׷��� �����͸� �Ǵ��ϴ� ����
    //���� ���Ǵ� �ڷ���: int, float, bool, string

    //����(variable): ���� ������ �� �ִ� ����
    //Ư�� �� �ϳ��� �����ϱ� ���� �̸��� ���� �������
    //����� ���
    //�ڷ��� ������(= ��);

    #endregion

    #region ����
    
    public float x;     //inspector���� �����ϴ� ��
    public float y;
    public float z;
    private int sample; //���ο��� ���ǹǷ� �ܺο��� �ǵ帱 �ʿ䰡 ���� ������
    #endregion

    #region �Լ�
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sample = 10;    //������ ������� ��� ��κ� �ڵ� ���ο��� ����
        Debug.Log(sample);  
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(x*Time.deltaTime,y*Time.deltaTime,z*Time.deltaTime));
        //deltaTime: �� �������� �Ϸ�Ǳ���� �ɸ��� �ð�
    }
    #endregion
}
