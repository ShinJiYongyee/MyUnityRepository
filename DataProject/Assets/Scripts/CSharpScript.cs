using UnityEngine;

//�ش� Ŭ������ ���� ��ü�� ����
class Unit
{
    public string unit_name;    //�ʵ�(Ŭ������ ����)

    //�޼ҵ�(�Լ�, Ŭ������ ������ ��ü�� ���)
    public static void UnitAction()
    {
        Debug.Log("������ �����մϴ�");
    }
    public void Cry()
    {
        Debug.Log("������ ���¢�����ϴ�");
    }
}
public class CSharpScript : MonoBehaviour
{
    Unit unit;  //�ش� Ŭ������ ��ü ����

    private void Start()
    {
        unit.unit_name = "MiniGun"; //��ü�� �ʵ� ����

        unit.Cry(); //�ν��Ͻ��� �޼ҵ� ���

        Unit.UnitAction();  
        //static �ʵ�/�޼ҵ�� ������ ��ü�� ȣ������ �ʰ�
        //Ŭ�����κ��� ȣ��
    }
}
