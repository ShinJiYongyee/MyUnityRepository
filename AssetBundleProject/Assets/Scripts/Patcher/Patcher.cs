using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Patcher : MonoBehaviour
{
    //��� �ۼ�
    string path = "Assets/Bundles/asset1";
    void Start()
    {
        StartCoroutine(LoadAsync(path));
    }

    IEnumerator LoadAsync(string path)
    {
        //AssetBundleCreateRequest : �񵿱� ���� ��û �Լ�
        AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path));

        //������Ʈ�� ���� ������ ���
        yield return request;

        //������Ʈ�� ���� �޾ƿ� ���� ������ ������ �����մϴ�.
        AssetBundle bundle = request.assetBundle;
        Debug.Log("patch completed");

        //���� ������ �޾ƿ��� �� ��ȯ
        if(bundle != null )
        {
            SceneManager.LoadScene("BRP Sample Scene");
            //�ּ� ���� �� asset1�� gameobject�� ������ ����
            GameObject prefab1 = bundle.LoadAsset<GameObject>("RedSphere");
            Instantiate(prefab1);
        }




    }
}
