using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public Dictionary<BGMType, AudioClip> bgmDic = new Dictionary<BGMType, AudioClip>();
    public Dictionary<SFXType, AudioClip> sfxDic = new Dictionary<SFXType, AudioClip>();
    public enum BGMType
    {
        Stage1BGM
    }
    public enum SFXType
    {
        Blocked,
        Damaged,
        Jump,
        PlayerDie,
        Run,
        SwordSwing
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    //���� ���� �� �ʱ�ȭ
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitSoundManager()
    {
        GameObject obj = new GameObject("SoundMangaer");
        Instance = obj.AddComponent<SoundManager>();
        DontDestroyOnLoad(obj);

        //BGM ����
        GameObject bgmObj = new GameObject("BGM");
        SoundManager.Instance.bgmSource = bgmObj.AddComponent<AudioSource>();
        bgmObj.transform.SetParent(obj.transform);
        SoundManager.Instance.bgmSource.loop = true;
        SoundManager.Instance.bgmSource.volume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);

        //SFX ����
        GameObject sfxObj = new GameObject("SFX");
        SoundManager.Instance.sfxSource = sfxObj.AddComponent<AudioSource>();
        SoundManager.Instance.sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        sfxObj.transform.SetParent(obj.transform);

        AudioClip[] bgmClips = Resources.LoadAll<AudioClip>("Sound/BGM");
        foreach (AudioClip clip in bgmClips)
        {
            try
            {
                BGMType type = (BGMType)Enum.Parse(typeof(BGMType), clip.name);
                SoundManager.Instance.bgmDic.Add(type, clip);
            }
            catch
            {
                Debug.LogWarning("BGM Enum �ʿ� : " + clip.name);
            }

        }

        AudioClip[] sfxClips = Resources.LoadAll<AudioClip>("Sound/SFX");
        foreach (var clip in sfxClips)
        {
            try
            {
                SFXType type = (SFXType)Enum.Parse(typeof(SFXType), clip.name);
                SoundManager.Instance.sfxDic.Add(type, clip);
            }
            catch
            {
                Debug.LogWarning("SFX Enum �ʿ� : " + clip.name);
            }
        }
        //Scene �ε� �ø��� OnSceneLoadCompleted ȣ��, +=�����ڷ� �̺�Ʈ �߰�
        SceneManager.sceneLoaded += SoundManager.Instance.OnSceneLoadCompleted;

    }

    //Scene��ȯ �Ϸ� �� �ڵ� ȣ��Ǵ� �Լ�
    public void OnSceneLoadCompleted(Scene scene, LoadSceneMode mode)
    {
        //Scene ��ȯ �� bgm ��ȯ
        if (scene.name == "Stage1")
        {
            PlayBGM(BGMType.Stage1BGM, 1f);
        }
        else if (scene.name == "Boss")
        {
            PlayBGM(BGMType.Stage1BGM, 1f);
        }
    }

    //ȿ���� ���
    public void PlaySFX(SFXType type)
    {
        sfxSource.PlayOneShot(sfxDic[type]);
    }

    //BGM ���
    public void PlayBGM(BGMType type, float fadeTime = 0)
    {
        if (bgmSource.clip != null)
        {
            if (bgmSource.clip.name == type.ToString())
            {
                return;
            }
            if (fadeTime == 0)
            {
                bgmSource.clip = bgmDic[type];
                bgmSource.Play();
            }
            else
            {
                StartCoroutine(FadeOutBGM(() =>
                {
                    bgmSource.clip = bgmDic[type];
                    bgmSource.Play();
                    StartCoroutine(FadeInBGM(fadeTime));
                }, fadeTime));
            }
        }
        else
        {
            if(fadeTime == 0)
            {
                bgmSource.clip = bgmDic[type];
                bgmSource.Play();
            }
            else
            {
                bgmSource.volume = 0;
                bgmSource.clip = bgmDic[type];
                bgmSource.Play();
                StartCoroutine(FadeInBGM(fadeTime));
            }
        }
    }

    private IEnumerator FadeOutBGM(Action onComplete, float duration)
    {
        float startVolume = bgmSource.volume;
        float time = 0;

        while(time < duration)
        {
            bgmSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        bgmSource.volume = 0f;
        onComplete.Invoke();
    }

    private IEnumerator FadeInBGM(float duration  = 1.0f)
    {
        //BGM ���� ������ �Ʒ� targetVolume�� �ٲ� ��
        float targetVolume = PlayerPrefs.GetFloat("BGMVolume", 0.2f);
        float time = 0;

        while(time < duration)
        {
            bgmSource.volume = Mathf.Lerp(0f, targetVolume, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        bgmSource.volume = targetVolume;
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
