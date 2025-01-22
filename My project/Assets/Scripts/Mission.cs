using UnityEngine;
public enum Rainbow
{
    빨, 주, 노, 초, 파, 남, 보
}
[AddComponentMenu("신지용/Mission")]
public class Mission : MonoBehaviour
{   
    public bool isJumpAvailable=true;
    public string[] fruitBasket;
    public int money;
    [RangeAttribute(1,99)]
    public int FoV;

    public Rainbow newRainbow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
