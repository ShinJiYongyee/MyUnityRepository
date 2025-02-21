using UnityEngine;

public enum ItemType
{
    arrow, key, life
}

public class ItemData : MonoBehaviour
{
    //부딫히면 획득하는 아이템 설정
    public ItemType type;
    public int count = 1;   //아이템의 개수
    public int arrangeId = 0;   //아이템을 구분하는 식별값

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //타입에 따라 처리할 내용
            switch (type)
            {
                case ItemType.arrow:
                    ArrowShoot shoot = collision.GetComponent<ArrowShoot>();
                    ItemKeeper.hasArrows += count;
                    break;
                case ItemType.key:
                    ItemKeeper.hasKeys += count;
                    break;
                case ItemType.life:
                    if (PlayerController.hp < 3)
                    {
                        PlayerController.hp++;
                    }
                    break;
            }
            //아이템 획득 시의 연출(Rigidbody 필요)
            GetComponent<CircleCollider2D>().enabled = false;   //아이템 콜라이더 비활성화
            var item_rbody = GetComponent<Rigidbody2D>();
            item_rbody.gravityScale = 2.5f;
            item_rbody.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);    //아이템이 튀어오르는 연출 활성화
            Destroy(gameObject, 0.5f);
        }
    }
}
