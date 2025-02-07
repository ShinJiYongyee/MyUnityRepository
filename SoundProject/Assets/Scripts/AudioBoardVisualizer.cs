using UnityEngine;
using UnityEngine.Audio;
//���带 �̿��� ������� ȭ�鿡�� ǥ���ϴ� ����
//AudioUI�� �����ϴ� ��ũ��Ʈ
public class AudioBoardVisualizer : MonoBehaviour
{
    //����� ����� Ŭ��
    public AudioClip audioClip;
    //����� ����� �ҽ�
    public AudioSource audioSource;
    //����� �ͼ� ����
    public AudioMixer audioMixer;

    //Board �迭
    public Board[] boards;

    //���� ������
    public float minBoard = 50.0f;
    public float maxBoard = 500.0f;
    //����Ʈ���� ����
    public int samples = 64;

    void Start()
    {
        //������Ʈ�� ����� �ڽ� ������Ʈ���� �������� �ڵ�
        boards = GetComponentsInChildren<Board>();
        //���� ������Ʈ�� ����� ������Ʈ�� ������ִ� �ڵ�
        if(audioSource == null)
        {
            //AudioSource�� ���� ���
            //���� �����ϰ� �ش� ������Ʈ�� AudioSource ������Ʈ �߰�
            audioSource = 
                new GameObject("AudioSource").AddComponent<AudioSource>();
        }
        else
        {
            //AudioSource�� ����� ������ ã�� ���
            audioSource = 
                GameObject.Find("AudioSource").GetComponent<AudioSource>();    
        }
        audioSource.clip = audioClip;
        //AudioSource�� ã�� ������ͼ��� Master�׷� �� 0��° ��Ҹ� ã�� ����
        audioSource.outputAudioMixerGroup = 
            audioMixer.FindMatchingGroups("Master")[0];
        audioSource.Play();
    }

    void Update()
    {
        //GetSpectrumData(Samples, channel, FFTWindow);
        //Samples = FFT(��ȣ�� ���� ���ļ� ����)�� ������ �迭��(2�� �������� ǥ��)
        //channel = �ִ� 8�� ä�� ����, ���� 0�� ���
        //FFTWindow�� ���ø� ����� ���� ��
        float[] data = 
            audioSource.GetSpectrumData(samples, 0, FFTWindow.Rectangular);

        //���� ������ ������ŭ �۾��� ����
        for (int i = 0; i < boards.Length; i++) 
        {
            //���� ������ ������
            var size = boards[i].GetComponent<RectTransform>().rect.size;

            size.y = minBoard + (data[i] * (maxBoard - minBoard) * 5.0f);

            //sizeDelta�� �θ� �������� �� ũ�� ���̸� ��Ÿ�� ��ġ
            boards[i].GetComponent<RectTransform>().sizeDelta = size;

        }
    }
}
