using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5.0f;
    Vector3 dir;
    public GameObject explosionFactory;

    private void Start()
    {

    }
    void Update()
    {
        var target = GameObject.FindGameObjectWithTag("Player");
        dir = target.transform.position - transform.position;
        dir.Normalize();    //������ ũ�⸦ 1�� ����
        transform.up = -dir;
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        int targetLayer = collision.gameObject.layer;

        if (targetLayer == LayerMask.NameToLayer("Player") ||
            targetLayer == LayerMask.NameToLayer("Bullet"))
        {
            GameObject explosion = Instantiate(explosionFactory);
            explosion.transform.position = transform.position;
            Destroy(collision.gameObject);  // ���� �ı�
            Destroy(gameObject);            // �ڽŵ� �ı�
        }
        else if (targetLayer == LayerMask.NameToLayer("Obstacle"))
        {
            GameObject explosion = Instantiate(explosionFactory);
            explosion.transform.position = transform.position;
            Destroy(gameObject);  // �ڽŸ� �ı�
        }
    }
}
