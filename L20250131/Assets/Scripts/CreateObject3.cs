using UnityEngine;

public class CreateObject3 : MonoBehaviour
{
    [SerializeField]private GameObject prefab;
    private int dummy;
    //����ȭ
    //�����ͳ� ������Ʈ�� �籸���� �� �ִ� ����(format)�� ��ȯ�ϴ� ����
    //����Ƽ���� �����ϰ� private������ �����͸� Inspector���� ���� �� �ְ� �������ش�
    [SerializeField] GameObject sample;
    void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Table_Body");
        //Resources.Load<T>("������ġ/���¸�")
        //T�� ���� �������� �ڷ���
        //Resources ���� �����ϴ� ����� Ư�� ������Ʈ�� ����

    }
    void Update()
    {
        ///if������ Ű �Է¹ޱ�        
        ///�Է¹��� Ű�� �����̽��� ���
        ///GetKeyDown(1ȸ �Է�)
        ///GetKeyUp(�Է� �� ����)
        ///GetKey(������ ����)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Sprite sprite = Resources.Load<Sprite>("Sprites/sprite01");
            //Resources/Sprites �Ʒ� sprite01�� �����Ƿ� �������� ����
            //���������� �̸� �ĺ����� ����
            sample=Instantiate(prefab);
            sample.AddComponent<VectorSample>();
            //AddComponent<T>
            //������Ʈ�� ������Ʈ ����� ������ ���
            //GetComponent<T>
            //������Ʈ�� ������ �ִ� ������Ʈ ����� ������ ���
            //��ũ��Ʈ���� �ش� ������Ʈ�� ����� ������ ����Ϸ� �� ��� ���
        }
    }
}
