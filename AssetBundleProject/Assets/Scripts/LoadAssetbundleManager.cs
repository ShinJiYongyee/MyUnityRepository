using System.Collections;
using System.IO;
using UnityEngine;

public class LoadAssetbundleManager : MonoBehaviour
{
    //경로 작성
    string path = "Assets/Bundles/asset1";
    void Start()
    {
        StartCoroutine(LoadAsync(path));
    }

    IEnumerator LoadAsync(string path)
    {
        //AssetBundleCreateRequest : 비동기 생성 요청 함수
        AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path));

        //리퀘스트가 끝날 때까지 대기
        yield return request;   

        //리퀘스트를 통해 받아온 에셋 번들의 정보를 적용합니다.
        AssetBundle bundle = request.assetBundle;

        //애셋 번들 내 asset1을 gameobject로 가져와 생성
        GameObject prefab1 = bundle.LoadAsset<GameObject>("RedSphere");
        Instantiate(prefab1);
    }
}
