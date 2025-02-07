using UnityEngine;

public class AudioSourceSample : MonoBehaviour
{
    //inspector에서 직접 연결하는 경우
    public AudioSource audioSourceBGM;

    //AudioSourceSample 객체가 자체적으로 오디오 소스를 가진 경우
    private AudioSource own_audioSource;

    //씬에서 찾아서 연결하는 경우
    public AudioSource audioSourceSFX;

    public AudioClip bgm;   //오디오 클립 연결

    void Start()
    {
        //GetComponent<T>로 얻어올 수 있다
        //own_audioSource = GetComponent<AudioSource>();

        //GameObject.Find("SFX").GetComponent<AudioSource>로 얻어올 수 있다
        //GameObject.Fine()는 씬에서 찾은 게임 오브젝트를 반환한다
        //GetComponent<T>로 해당 오브젝트가 가진 컴포넌트를 반환
        //play와 동시에 자동으로 오브젝트 SFX에 연결
        audioSourceSFX = GameObject.Find("SFX").GetComponent<AudioSource>();

        audioSourceBGM.clip = bgm;  //오디오 클립을 bgm으로 연결
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
