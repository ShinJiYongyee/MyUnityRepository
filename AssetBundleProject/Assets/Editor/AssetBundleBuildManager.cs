using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundleBuildManager 
{
    //에디터에 메뉴를 등록해주는 기능
    [MenuItem("Asset Bundle/Build")]
    public static void AssetBundleBuild()
    {
        //현재 번들의 위치
        string directory = "Assets/Bundles";

        //해당 디렉토리가 존재하지 않는다면
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("디렉토리" + directory + " 생성");
        }

        //해당 경로에 에셋 번들 옵션과 빌드 플랫폼을 설정해 빌드 진행
        BuildPipeline.BuildAssetBundles(directory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

        //에디터에서 보여주는 다이얼로그 창(타이틀, 내용, 확인 메세지)
        EditorUtility.DisplayDialog("Asset Bundle Build", "Asset Bundle build completed", "complete");

        Debug.Log("Asset bundle build completed");
    }
}
