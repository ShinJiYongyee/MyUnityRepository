using UnityEngine;

public class MaterialChange : MonoBehaviour
{

    //Material을 바꾸는 스크립트

    public Material skyboxMaterial;

    void Start()
    {
        ChangeMaterial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeMaterial()
    {
        RenderSettings.skybox = skyboxMaterial;

    }
}
