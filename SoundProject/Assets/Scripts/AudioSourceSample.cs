using UnityEngine;

public class AudioSourceSample : MonoBehaviour
{
    //inspector���� ���� �����ϴ� ���
    public AudioSource audioSourceBGM;

    //AudioSourceSample ��ü�� ��ü������ ����� �ҽ��� ���� ���
    private AudioSource own_audioSource;

    //������ ã�Ƽ� �����ϴ� ���
    public AudioSource audioSourceSFX;

    public AudioClip bgm;   //����� Ŭ�� ����

    void Start()
    {
        //GetComponent<T>�� ���� �� �ִ�
        //own_audioSource = GetComponent<AudioSource>();

        //GameObject.Find("SFX").GetComponent<AudioSource>�� ���� �� �ִ�
        //GameObject.Fine()�� ������ ã�� ���� ������Ʈ�� ��ȯ�Ѵ�
        //GetComponent<T>�� �ش� ������Ʈ�� ���� ������Ʈ�� ��ȯ
        //play�� ���ÿ� �ڵ����� ������Ʈ SFX�� ����
        audioSourceSFX = GameObject.Find("SFX").GetComponent<AudioSource>();

        audioSourceBGM.clip = bgm;  //����� Ŭ���� bgm���� ����
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
