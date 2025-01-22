using UnityEngine;

public enum Rainbow
{
    빨, 주, 노, 초, 파, 남, 보
}

[AddComponentMenu("신지용/Sample01")]
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
