using UnityEngine;

public class MaterialChange : MonoBehaviour
{

    //Material�� �ٲٴ� ��ũ��Ʈ

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
