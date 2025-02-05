using UnityEngine;

public class MaterialChange : MonoBehaviour
{

    //Material을 바꾸는 스크립트
    public Material skyboxMaterial;

    void Start()
    {
        RenderSettings.skybox=skyboxMaterial;
    }

    void Update()
    {
        
    }
}
