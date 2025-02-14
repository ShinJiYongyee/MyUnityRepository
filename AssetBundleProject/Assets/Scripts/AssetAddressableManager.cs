using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AssetAddressableManager : MonoBehaviour
{
    //AssetReference는 특정 타입을 지정하지 않고 addressable의 리소스를 참조한다
    //AssetReferenceGameObject는 addressable의 리소스 중 GameObject 리소스를 참조한다
    //AssetReferenceT는 제네릭을 이용해 특정 형태의 addressable 리소스를 참조한다
    public AssetReferenceGameObject capsule;
    //public AssetReferenceT<GameManager> manager;

    GameObject go;

    private void Start()
    {
        go = new GameObject();

        StartCoroutine("Init"); 
    }

    //비동기 코드 -> 실행 중 다른 작업을 허용할 수 있다
    private IEnumerator Init()
    {
        var init = Addressables.InitializeAsync();
        yield return init;  
    }
    public void OnCreateButtonEnter()
    {
        capsule.InstantiateAsync().Completed += (obj) =>
        {
            go = obj.Result;
        };
    }
    public void OnReleaseButtonEnter()
    {
        Addressables.ReleaseInstance(go);   //데이터 해제
    }
}
