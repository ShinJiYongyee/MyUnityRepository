using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource BGMSource;
    public AudioSource SFXSource;

    //Unity에서는 Editor 상에서 Dictionary를 열람할 수 없다
    private Dictionary<string, AudioClip> BGMClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> SFXClips = new Dictionary<string, AudioClip>();

    //따라서 열거형 구조체를 만들어 그 안에 Dictionary의 요소를 넣어 열람할 수 있게 한다.
    [Serializable]
    public struct NamedAudioClip
    {
        public string name;
        public AudioClip clip;
    }

    public NamedAudioClip[] BGMClipList;
    public NamedAudioClip[] SFXClipList;

    private Coroutine currentBGMCoroutine; //현재 BGM 재생 여부 판단

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

    private void Start()
    {
        //맨 첫번 Scene 검사
        string activeSceneName = SceneManager.GetActiveScene().name;
        if(activeSceneName == "Menu")
        {
            PlayBGM("MenuBGM", 1.0f);
        }
        else if(activeSceneName == "Stage1")
        {
            PlayBGM("Stage1BGM", 1.0f);
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

    public void PlayBGM(string name, float fadeDuration = 1.0f)
    {
        if (BGMClips.ContainsKey(name))
        {
            if(currentBGMCoroutine != null)
            {
                StopCoroutine(currentBGMCoroutine); //BGM이 이미 켜져 있다면 끄기
            }

            currentBGMCoroutine = StartCoroutine(FadeOutBGM(fadeDuration, () =>
            {
                BGMSource.spatialBlend = 0f; //거리에 따른 음향 효과 제거
                BGMSource.clip = BGMClips[name];
                BGMSource.Play();
                currentBGMCoroutine = StartCoroutine(FadeInBGM(fadeDuration));
            })); //람다식을 이용한 BGM 재생
        }
    }

    public void PlaySFX(string name)
    {
        if (SFXClips.ContainsKey(name))
        {
            SFXSource.PlayOneShot(SFXClips[name]);
        }
    }
    public void PlaySFX(string name, Vector3 position)
    {
        if (SFXClips.ContainsKey(name))
        {
            AudioSource.PlayClipAtPoint(SFXClips[name], position); //특정 위치의 오디오클립 재생
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

    private IEnumerator FadeOutBGM(float duration, Action onFadeComplete)
    {
        float startVolume = BGMSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            BGMSource.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }
        BGMSource.volume = 0;
        onFadeComplete?.Invoke();   //페이드 아웃 완료시 다음 작업 실행
    }

    private IEnumerator FadeInBGM(float duration)
    {
        float startVolume = 0f;
        BGMSource.volume = 0f;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            BGMSource.volume = Mathf.Lerp(startVolume, 1f, t / duration);
            yield return null;
        }
        BGMSource.volume = 1.0f;

    }
}
