using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GoogleDriveAssetBundle : MonoBehaviour
{
    //��ũ �̽��� ���� �Ұ�
    private string imageFileURL = "https://docs.google.com/uc?export=download&id=1xjTidSsG6e9W9ocmsFb6vuk3cLDLhY40";

    public Image image;
    
    void Start()
    {
        StartCoroutine("DownLoadImage");
    }

    IEnumerator DownLoadImage()
    {
        //����Ƽ���� ���� �ؽ��ĸ� ��û
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageFileURL);

        //������Ʈ �Ϸ� �ñ��� ���
        yield return www.SendWebRequest();

        //������Ʈ ����� ������ ���
        if(www.result == UnityWebRequest.Result.Success)
        {
            var texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1.0f);
            Debug.Log("�̹����� ���������� �����Խ��ϴ�");

            image.sprite = sprite;
        }
        else
        {
            Debug.LogError("�̹����� �������� ���߽��ϴ�");
        }
    }
}
