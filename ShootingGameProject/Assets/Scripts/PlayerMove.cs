using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5.0f;
    Rigidbody rb;
    public GameObject explosionFactory;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, v, 0);
        transform.position += dir * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (h == 0 && v == 0)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("InvisibleWall"))  // 투명벽과 충돌했을 때
        {
            rb = GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;  // 속도 초기화
        }
        int targetLayer = collision.gameObject.layer;

        if (targetLayer == LayerMask.NameToLayer("Enemy") ||
            targetLayer == LayerMask.NameToLayer("Obstacle"))
        {
            GameObject explosion = Instantiate(explosionFactory);
            explosion.transform.position = transform.position;
            Destroy(gameObject);  // 자신만 파괴
        }
    }

}
