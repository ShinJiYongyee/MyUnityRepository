using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AudioSourceSample : MonoBehaviour
{

    public AudioClip bgm;   //오디오 클립 연결

    //스크립트에 사운드를 연결하는 방법

    //1. inspector에서 직접 연결하는 경우
    public AudioSource audioSourceBGM;

    //2. AudioSourceSample 객체가 자체적으로 오디오 소스를 가진 경우
    private AudioSource own_audioSource;

    //3. 씬에서 찾아서 연결하는 경우
    public AudioSource audioSourceSFX;

    //4. Resources.Load() 기능을 이용해 리소스 폴더에 있는 오디오 소스의 클립을 받아오기


    void Start()
    {

        audioSourceBGM.clip = bgm;  //오디오 클립을 bgm으로 연결

        //2. GetComponent<T>로 얻어올 수 있다
        //own_audioSource = GetComponent<AudioSource>();

        //3. GameObject.Find("SFX").GetComponent<AudioSource>로 얻어올 수 있다
        //GameObject.Fine()는 씬에서 찾은 게임 오브젝트를 반환한다
        //GetComponent<T>로 해당 오브젝트가 가진 컴포넌트를 반환
        //play와 동시에 자동으로 오브젝트 SFX에 연결
        audioSourceSFX = GameObject.Find("SFX").GetComponent<AudioSource>();

        //4. Resources.Load(파일명) 은 리소스 폴더에서 오브젝트를 찾아 로드
        //반환값이 Object이므로,
        //as 클래스명 을 통해 해당 데이터의 자료형을 표현해 그 형태로 받아오기
        //audioSourceSFX.clip = Resources.Load("Explosion") as AudioClip;

        //리소스 로드기 경로가 정해져 있다면 폴더명/파일명(확장자 불필요) 으로 작성
        audioSourceSFX.clip = Resources.Load("Audio/BombCharge") as AudioClip;

        ////UnityWebRequst의 GetAudioClip 기능 활용
        //StartCoroutine("GetAudioClip");

        //audioSourceSFX.Play();              //클립을 실행하는 도구

    }

    //IEnumerator GetAudioClip()
    //{
    //    //파일명+에셋명+파일경로/파일명
    //    UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(
    //       "file:///" + Application.dataPath + "/Audio/Explosion.wav",AudioType.WAV);

    //    //전달
    //    yield return uwr.SendWebRequest();

    //    //받은 경로를 기반으로 다운로드 진행
    //    var new_clip = DownloadHandlerAudioClip.GetContent(uwr);

    //    audioSourceBGM.clip= new_clip;  //클립 등록
    //    audioSourceBGM.Play();          //플레이
    //}
    //작업 종료후 값 제거

    // Update is called once per frame
    void Update()
    {
        
    }
}
