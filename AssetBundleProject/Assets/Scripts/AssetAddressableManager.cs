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
    GameObject go=null; 
    private void Start()
    {
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
        if (go != null)
        {
            Debug.Log("�̹� �ν��Ͻ�ȭ�� ������Ʈ�� �ֽ��ϴ�!");
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
            Debug.Log("������ ������Ʈ�� �����ϴ�!");
            return;
        }
        Addressables.ReleaseInstance(go);
        go = null;  // ���� �ʱ�ȭ
    }
}
