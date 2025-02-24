using UnityEngine;

public enum ItemType
{
    arrow, key, life
}

public class ItemData : MonoBehaviour
{
    //�΋H���� ȹ���ϴ� ������ ����
    public ItemType type;
    public int count = 1;   //�������� ����
    public int arrangeId = 0;   //�������� �����ϴ� �ĺ���

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Ÿ�Կ� ���� ó���� ����
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
            //������ ȹ�� ���� ����(Rigidbody �ʿ�)
            GetComponent<CircleCollider2D>().enabled = false;   //������ �ݶ��̴� ��Ȱ��ȭ
            var item_rbody = GetComponent<Rigidbody2D>();
            item_rbody.gravityScale = 2.5f;
            item_rbody.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);    //�������� Ƣ������� ���� Ȱ��ȭ
            Destroy(gameObject, 0.5f);
        }
    }
}
