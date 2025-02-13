using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu]
public class AudioEvent : ScriptableObject
{
    public event Action<string> OnPlay;

    public void Play(string name)
    {
        if (OnPlay != null)
        {
            OnPlay.Invoke(name);    //Invoke : 이벤트 실행용 함수
        }
    }
}
