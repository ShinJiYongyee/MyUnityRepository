using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GoogleDriveAssetBundle : MonoBehaviour
{
    //링크 이슈로 구동 불가
    private string imageFileURL = "https://docs.google.com/uc?export=download&id=1xjTidSsG6e9W9ocmsFb6vuk3cLDLhY40";

    public Image image;
    
    void Start()
    {
        StartCoroutine("DownLoadImage");
    }

    IEnumerator DownLoadImage()
    {
        //유니티에서 웹에 텍스쳐를 요청
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageFileURL);

        //리퀘스트 완료 시까지 대기
        yield return www.SendWebRequest();

        //리퀘스트 결과가 성공일 경우
        if(www.result == UnityWebRequest.Result.Success)
        {
            var texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1.0f);
            Debug.Log("이미지를 성공적으로 가져왔습니다");

            image.sprite = sprite;
        }
        else
        {
            Debug.LogError("이미지를 가져오지 못했습니다");
        }
    }
}
