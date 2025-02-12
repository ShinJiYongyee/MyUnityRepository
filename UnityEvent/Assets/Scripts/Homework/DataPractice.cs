using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
public interface GameItem
{
    string Name { get; set; }
}
public interface Weapon
{
    int Power {  get; set; }
    public void Attack();
}
public class Sword : Weapon, GameItem
{
    int power;
    string name;
    public int Power
    {
        get
        {
            return power;
        }
        set
        {
            power = value;
        }
    }
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
    public void Attack()
    {
        Debug.Log("Slash");
    }
}
public class BlazingSword : Sword
{
    public new void Attack()
    {
        Debug.Log("FireSlash");
    }
}
public class DataPractice : MonoBehaviour
{
    Sword sword = new Sword();
    void Start()
    {
        sword.Name = "БэЗа°Л";
        sword.Power = 1;
        sword.Attack();
    }
}
