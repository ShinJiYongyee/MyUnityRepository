using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource BGMSource;
    public AudioSource SFXSource;

    //Unity������ Editor �󿡼� Dictionary�� ������ �� ����
    private Dictionary<string, AudioClip> BGMClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> SFXClips = new Dictionary<string, AudioClip>();

    //���� ������ ����ü�� ����� �� �ȿ� Dictionary�� ��Ҹ� �־� ������ �� �ְ� �Ѵ�.
    [Serializable]
    public struct NamedAudioClip
    {
        public string name;
        public AudioClip clip;
    }

    public NamedAudioClip[] BGMClipList;
    public NamedAudioClip[] SFXClipList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioClips();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeAudioClips()
    {
        foreach (var BGM in BGMClipList)
        {
            if (!BGMClips.ContainsKey(BGM.name))
            {
                BGMClips.Add(BGM.name, BGM.clip);
            }
        }
        foreach (var SFX in SFXClipList)
        {
            if (!SFXClips.ContainsKey(SFX.name))
            {
                SFXClips.Add(SFX.name, SFX.clip);
            }
        }
    }

    public void PlayBGM(string name)
    {
        if (BGMClips.ContainsKey(name))
        {
            BGMSource.clip = BGMClips[name];
            BGMSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        if (SFXClips.ContainsKey(name))
        {
            SFXSource.PlayOneShot(SFXClips[name]);
        }
    }

    public void StopBGM()
    {
        BGMSource.Stop();
    }

    public void StopSFX()
    {
        SFXSource.Stop();
    }
    public void SetBGMVolume(float volume)
    {
        BGMSource.volume = Mathf.Clamp(volume, 0, 1);
    }
    public void SetSFXVolume(float volume)
    {
        SFXSource.volume = Mathf.Clamp(volume, 0, 1);
    }
}
