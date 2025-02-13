using System;
using UnityEngine;

/// 퀘스트 유형
public enum QuestType
{
    normal,     //클리어 후 재클리어 불가
    daily,      //매일 퀘스트 갱신
    weekly      //매주 퀘스트 갱신
}

[CreateAssetMenu(fileName ="Quest",menuName ="Quest/quest")]
public class Quest : ScriptableObject
{
    public QuestType 퀘스트유형;
    public Reward 보상;
    public Requirement 요구조건;

    [Header("퀘스트 정보")]
    public string 제목;    //퀘스트명
    public string 목표;    //퀘스트 목표
    [TextArea] public string 설명;    //퀘스트 설명

    public bool 성공;     //퀘스트 성공 여부
    public bool 진행상태;   //퀘스트 진행 여부
}

/// 요구 조건에 대한 스크립터블 오브젝트
[CreateAssetMenu(fileName ="Quest",menuName ="Quest/Requirement")]
public class Requirement : ScriptableObject
{
    public int 목표몬스터수;
    public int 현재잡은몬스터수;
}

[Serializable]
[CreateAssetMenu(fileName = "Quest", menuName = "Quest/Reward")]
public class Reward : ScriptableObject
{
    public int 돈;
    public float 경험치;

}