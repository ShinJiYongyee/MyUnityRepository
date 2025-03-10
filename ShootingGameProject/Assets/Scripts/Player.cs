using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    Rigidbody rb;
    public GameObject explosionFactory;

    public GameObject bullet;    //�Ѿ� ������
    public GameObject firePosition;     //�� �߻� ��ġ
    public GameObject firePositionRightInner;     //�� �߻� ��ġ
    public GameObject firePositionLeftInner;     //�� �߻� ��ġ
    public GameObject firePositionRightOuter;     //�� �߻� ��ġ
    public GameObject firePositionLeftOuter;     //�� �߻� ��ġ


    float playerStopped;
    float originalPenalty;
    public static Vector3 direction;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerStopped = 0;
        originalPenalty = GameManager.instance.penalty;
    }

    void Update()
    {
        if (!GameManager.instance.isGameOver && !GameManager.instance.isGameClear)
        {
            PlayerAim();
            PlayerFire();
            PlayerMove();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("InvisibleWall"))  // �������� �浹���� ��
        {
            rb = GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;  // �ӵ� �ʱ�ȭ
        }
        int targetLayer = collision.gameObject.layer;

        if (targetLayer == LayerMask.NameToLayer("Enemy") ||
            targetLayer == LayerMask.NameToLayer("Obstacle"))
        {
            GameObject explosion = Instantiate(explosionFactory);
            explosion.transform.position = transform.position;
            Destroy(gameObject);  // �ڽŸ� �ı�
            GameManager.instance.isGameOver = true;
        }
    }
    void PlayerFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //�Ѿ� ����
            GameObject bulletObject = Instantiate(bullet);
            //�Ѿ� ��ġ ����
            bulletObject.transform.position = firePosition.transform.position;

            if (ScoreManager.currentStage >= 2)
            {
                GameObject bulletObjectRightInner = Instantiate(bullet);
                GameObject bulletObjectLeftInner = Instantiate(bullet);
                bulletObjectRightInner.transform.position = firePositionRightInner.transform.position;
                bulletObjectLeftInner.transform.position = firePositionLeftInner.transform.position;
            }
            if (ScoreManager.currentStage >= 3)
            {
                GameObject bulletObjectRightOuter = Instantiate(bullet);
                GameObject bulletObjectLeftOuter = Instantiate(bullet);
                bulletObjectRightOuter.transform.position = firePositionRightOuter.transform.position;
                bulletObjectLeftOuter.transform.position = firePositionLeftOuter.transform.position;
            }
        }
    }
    void PlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, v, 0);
        transform.position += dir * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        PlayerStoppedTimeAdd(h, v);

    }
    void PlayerAim()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        direction = new Vector3(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y, 0);

        transform.up = direction;
    }
    void PlayerStoppedTimeAdd(float h, float v)
    {
        if (h == 0 && v == 0)
        {
            rb.linearVelocity = Vector3.zero;
            if (playerStopped <= GameManager.instance.maxPenalty && (!GameManager.instance.isGameOver && !GameManager.instance.isGameClear))
            {
                playerStopped += (Time.deltaTime / 100.0f);
            }
            else
            {
                playerStopped = 0;
            }
            GameManager.instance.penalty += playerStopped;
        }
        else
        {
            GameManager.instance.penalty = originalPenalty;

        }
    }

}
