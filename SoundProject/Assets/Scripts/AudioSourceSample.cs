using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AudioSourceSample : MonoBehaviour
{

    public AudioClip bgm;   //����� Ŭ�� ����

    //��ũ��Ʈ�� ���带 �����ϴ� ���

    //1. inspector���� ���� �����ϴ� ���
    public AudioSource audioSourceBGM;

    //2. AudioSourceSample ��ü�� ��ü������ ����� �ҽ��� ���� ���
    private AudioSource own_audioSource;

    //3. ������ ã�Ƽ� �����ϴ� ���
    public AudioSource audioSourceSFX;

    //4. Resources.Load() ����� �̿��� ���ҽ� ������ �ִ� ����� �ҽ��� Ŭ���� �޾ƿ���


    void Start()
    {

        audioSourceBGM.clip = bgm;  //����� Ŭ���� bgm���� ����

        //2. GetComponent<T>�� ���� �� �ִ�
        //own_audioSource = GetComponent<AudioSource>();

        //3. GameObject.Find("SFX").GetComponent<AudioSource>�� ���� �� �ִ�
        //GameObject.Fine()�� ������ ã�� ���� ������Ʈ�� ��ȯ�Ѵ�
        //GetComponent<T>�� �ش� ������Ʈ�� ���� ������Ʈ�� ��ȯ
        //play�� ���ÿ� �ڵ����� ������Ʈ SFX�� ����
        audioSourceSFX = GameObject.Find("SFX").GetComponent<AudioSource>();

        //4. Resources.Load(���ϸ�) �� ���ҽ� �������� ������Ʈ�� ã�� �ε�
        //��ȯ���� Object�̹Ƿ�,
        //as Ŭ������ �� ���� �ش� �������� �ڷ����� ǥ���� �� ���·� �޾ƿ���
        //audioSourceSFX.clip = Resources.Load("Explosion") as AudioClip;

        //���ҽ� �ε�� ��ΰ� ������ �ִٸ� ������/���ϸ�(Ȯ���� ���ʿ�) ���� �ۼ�
        audioSourceSFX.clip = Resources.Load("Audio/BombCharge") as AudioClip;

        ////UnityWebRequst�� GetAudioClip ��� Ȱ��
        //StartCoroutine("GetAudioClip");

        //audioSourceSFX.Play();              //Ŭ���� �����ϴ� ����

    }

    //IEnumerator GetAudioClip()
    //{
    //    //���ϸ�+���¸�+���ϰ��/���ϸ�
    //    UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(
    //       "file:///" + Application.dataPath + "/Audio/Explosion.wav",AudioType.WAV);

    //    //����
    //    yield return uwr.SendWebRequest();

    //    //���� ��θ� ������� �ٿ�ε� ����
    //    var new_clip = DownloadHandlerAudioClip.GetContent(uwr);

    //    audioSourceBGM.clip= new_clip;  //Ŭ�� ���
    //    audioSourceBGM.Play();          //�÷���
    //}
    //�۾� ������ �� ����

    // Update is called once per frame
    void Update()
    {
        
    }
}
