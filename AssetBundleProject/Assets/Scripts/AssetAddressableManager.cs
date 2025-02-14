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
    GameObject go=null; 
    private void Start()
    {
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
        if (go != null)
        {
            Debug.Log("이미 인스턴스화된 오브젝트가 있습니다!");
            return;
        }

        capsule.InstantiateAsync().Completed += (obj) =>
        {
            go = obj.Result;
        };
    }
    public void OnReleaseButtonEnter()
    {
        if (go == null)
        {
            Debug.Log("해제할 오브젝트가 없습니다!");
            return;
        }
        Addressables.ReleaseInstance(go);
        go = null;  // 참조 초기화
    }
}
