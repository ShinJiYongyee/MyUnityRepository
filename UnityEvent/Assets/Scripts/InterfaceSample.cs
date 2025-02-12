using UnityEngine;
using UnityEngine.UIElements;
public interface ICountAble
{
    public int Count { get; set; }

    void CountPlus();

    //int a = 0;  //�������̽������� �ν��Ͻ�ȭ �Ұ�, ���� ����
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
                Debug.Log("count�� 99���� �ִ�");
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
        Debug.Log($"{name}�� ����߽��ϴ�");
        Count--;
    }
}

public class InterfaceSample : MonoBehaviour
{
    Potion potion = new Potion();

    void Start()
    {
        //�ϼ��� Ŭ���� ��� �ϱ�
        potion.Count = 99;
        potion.Name = "���� ����";
        potion.CountPlus();
        potion.Use();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
