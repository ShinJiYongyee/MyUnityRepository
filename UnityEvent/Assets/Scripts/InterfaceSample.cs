using UnityEngine;
using UnityEngine.UIElements;
public interface ICountAble
{
    public int Count { get; set; }

    void CountPlus();

    //int a = 0;  //인터페이스에서는 인스턴스화 불가, 선언만 가능
}

public interface IUseAble
{
    void Use();
}



class Potion : Item, ICountAble, IUseAble
{
    private int count;
    private string name;
    public int Count
    {
        get
        {
            return count;
        }
        set
        {
            if ( count > 99 )
            {
                Debug.Log("count는 99개가 최대");
                count = 99;
            }
            count = value;
        }
    }

    public string Name { get => name; set => name = value; }

    public void CountPlus()
    {
        Count++;
    }

    public void Use()
    {
        Debug.Log($"{name}을 사용했습니다");
        Count--;
    }
}

public class InterfaceSample : MonoBehaviour
{
    Potion potion = new Potion();

    void Start()
    {
        //완성된 클래스 사용 하기
        potion.Count = 99;
        potion.Name = "빨간 포션";
        potion.CountPlus();
        potion.Use();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
