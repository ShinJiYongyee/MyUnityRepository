using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AssetAddressableManager : MonoBehaviour
{
    //AssetReference�� Ư�� Ÿ���� �������� �ʰ� addressable�� ���ҽ��� �����Ѵ�
    //AssetReferenceGameObject�� addressable�� ���ҽ� �� GameObject ���ҽ��� �����Ѵ�
    //AssetReferenceT�� ���׸��� �̿��� Ư�� ������ addressable ���ҽ��� �����Ѵ�
    public AssetReferenceGameObject capsule;
    //public AssetReferenceT<GameManager> manager;

    GameObject go;

    private void Start()
    {
        go = new GameObject();

        StartCoroutine("Init"); 
    }

    //�񵿱� �ڵ� -> ���� �� �ٸ� �۾��� ����� �� �ִ�
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
        Addressables.ReleaseInstance(go);   //������ ����
    }
}
