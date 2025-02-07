using UnityEngine;
using UnityEngine.Audio;
//보드를 이용해 오디오를 화면에서 표현하는 도구
//AudioUI에 적용하는 스크립트
public class AudioBoardVisualizer : MonoBehaviour
{
    //사용할 오디오 클립
    public AudioClip audioClip;
    //사용할 오디오 소스
    public AudioSource audioSource;
    //오디오 믹서 연결
    public AudioMixer audioMixer;

    //Board 배열
    public Board[] boards;

    //보드 증가량
    public float minBoard = 50.0f;
    public float maxBoard = 500.0f;
    //스펙트럼용 샘플
    public int samples = 64;

    void Start()
    {
        //오브젝트에 연결된 자식 컴포넌트들을 가져오는 코드
        boards = GetComponentsInChildren<Board>();
        //게임 오브젝트를 만들어 컴포넌트를 등록해주는 코드
        if(audioSource == null)
        {
            //AudioSource가 없을 경우
            //새로 생성하고 해당 오브젝트에 AudioSource 컴포넌트 추가
            audioSource = 
                new GameObject("AudioSource").AddComponent<AudioSource>();
        }
        else
        {
            //AudioSource가 존재시 씬에서 찾아 등록
            audioSource = 
                GameObject.Find("AudioSource").GetComponent<AudioSource>();    
        }
        audioSource.clip = audioClip;
        //AudioSource를 찾아 오디오믹서의 Master그룹 중 0번째 요소를 찾아 적용
        audioSource.outputAudioMixerGroup = 
            audioMixer.FindMatchingGroups("Master")[0];
        audioSource.Play();
    }

    void Update()
    {
        //GetSpectrumData(Samples, channel, FFTWindow);
        //Samples = FFT(신호에 대한 주파수 영역)를 저장할 배열값(2의 제곱수로 표현)
        //channel = 최대 8개 채널 지원, 보통 0을 사용
        //FFTWindow는 샘플링 진행시 쓰는 값
        float[] data = 
            audioSource.GetSpectrumData(samples, 0, FFTWindow.Rectangular);

        //보드 각각의 개수만큼 작업을 진행
        for (int i = 0; i < boards.Length; i++) 
        {
            //개별 보드의 사이즈
            var size = boards[i].GetComponent<RectTransform>().rect.size;

            size.y = minBoard + (data[i] * (maxBoard - minBoard) * 5.0f);

            //sizeDelta는 부모를 기준으로 한 크기 차이를 나타낸 수치
            boards[i].GetComponent<RectTransform>().sizeDelta = size;

        }
    }
}
