using System;
using UnityEngine;

/// ����Ʈ ����
public enum QuestType
{
    normal,     //Ŭ���� �� ��Ŭ���� �Ұ�
    daily,      //���� ����Ʈ ����
    weekly      //���� ����Ʈ ����
}

[CreateAssetMenu(fileName ="Quest",menuName ="Quest/quest")]
public class Quest : ScriptableObject
{
    public QuestType ����Ʈ����;
    public Reward ����;
    public Requirement �䱸����;

    [Header("����Ʈ ����")]
    public string ����;    //����Ʈ��
    public string ��ǥ;    //����Ʈ ��ǥ
    [TextArea] public string ����;    //����Ʈ ����

    public bool ����;     //����Ʈ ���� ����
    public bool �������;   //����Ʈ ���� ����
}

/// �䱸 ���ǿ� ���� ��ũ���ͺ� ������Ʈ
[CreateAssetMenu(fileName ="Quest",menuName ="Quest/Requirement")]
public class Requirement : ScriptableObject
{
    public int ��ǥ���ͼ�;
    public int �����������ͼ�;
}

[Serializable]
[CreateAssetMenu(fileName = "Quest", menuName = "Quest/Reward")]
public class Reward : ScriptableObject
{
    public int ��;
    public float ����ġ;

}