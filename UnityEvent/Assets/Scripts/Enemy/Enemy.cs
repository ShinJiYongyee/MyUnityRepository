using UnityEngine;

public interface Enemy
{
    void Action();
}

public class Goblin : Enemy
{
    public void Action()
    {
        Debug.Log("고블린이 공격을 합니다");
    }
}

public class Slime : Enemy
{
    public void Action()
    {
        Debug.Log("슬라임이 점프 공격을 합니다");
    }
}

public class Wolf : Enemy
{
    public void Action()
    {
        Debug.Log("늑대가 물어뜯기 공격을 합니다");
    }
}
