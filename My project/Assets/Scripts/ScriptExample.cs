using UnityEngine;

/// <summary>
/// Unity Attribute
/// ����Ƽ������ �����Ϳ� �°� ��ũ��Ʈ�� Ŀ������ �� ����
/// </summary>
/// 

[AddComponentMenu("CustomUtility/ScriptExample")]
public class ScriptExample : MonoBehaviour
{
    [Range(1, 99)]
    public int level;

    [Range(0,100)]
    public int volume;

    [Header("�÷��̾��� �̸�")]
    public string player_name;
    public string player_description;

    [TextArea]
    public string talk01;

    [TextArea(1,3)]
    public string talk02;

    [TextArea(5,7)]
    public string talk03;

    //bool=���ǹ��� �����ϴ� ����
    //�������� ���� � �ۿ�
    [Tooltip("üũ�Ǹ� ���� �������� �ǹ��մϴ�.")]
    public bool isDead = true;

    //�Լ�(Function)
    //Ŭ���� ���� �Լ�=�޼ҵ�(method)
    //�Լ��� Ư���� ���� ����� �����ϱ� ���� �ۼ��� ��ɹ� ����ü
    //�ڵ� ������ ����� �Լ��� ���ϴ� �۾� ��ġ���� ȣ���� ���
    //�ڷ��� �Լ���(�Ű�����){�Լ� ȣ��� ������ ��ɹ�}
    //�Լ���(����);
    //�Ű�����: �Լ� ȣ�� �� ���� ���� ������ 
    //����: �Լ��� ȣ���� �� ������ ��

    [ContextMenu("HelloEveryone")]
    void HelloEveryone()
    {
        Debug.Log("�����е� ��� �ȳ��ϼ���!!");
    }

    void HelloSomeone(string who) 
    { 
        Debug.Log($"{who}�� �ȳ��ϼ���!");
    }

}
