using UnityEngine;

public class MaterialChange : MonoBehaviour
{

    //Material�� �ٲٴ� ��ũ��Ʈ
    public Material skyboxMaterial;

    void Start()
    {
        RenderSettings.skybox=skyboxMaterial;
    }

    void Update()
    {
        
    }
}
