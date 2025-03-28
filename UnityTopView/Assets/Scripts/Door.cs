using UnityEngine;

public class Door : MonoBehaviour
{
    public int arrangeId = 0;   //식별값

    //플레이어가 열쇠를 가지고 있을 경우, 열쇠 1개를 소모해 입장할 수 있도록 구현

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(ItemKeeper.hasKeys > 0)
        {
            ItemKeeper.hasKeys--;
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("열쇠가 없습니다");
        }
    }
}
