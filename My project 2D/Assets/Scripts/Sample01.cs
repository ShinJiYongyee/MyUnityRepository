using UnityEngine;

public enum Rainbow
{
    ��, ��, ��, ��, ��, ��, ��
}

[AddComponentMenu("������/Sample01")]
public class Sample01 : MonoBehaviour
{
    public bool isJumpAvailable;
    public string[] FruitBasket;
    public int money;
    [Range(1, 99)]public float FoV;
    public Rainbow rainbow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
