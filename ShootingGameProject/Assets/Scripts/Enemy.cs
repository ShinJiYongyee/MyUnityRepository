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
        if (!GameManager.instance.isGameOver && !GameManager.instance.isGameClear)
        {
            SearchPlayer();
        }

    }
    void SearchPlayer()
    {
        var target = GameObject.FindGameObjectWithTag("Player");
        dir = target.transform.position - transform.position;
        dir.Normalize();    //방향의 크기를 1로 설정
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

            Destroy(collision.gameObject);  // 상대방 파괴
            Destroy(gameObject);            // 자신도 파괴

            ScoreManager.currentScore++;
        }
        else if (targetLayer == LayerMask.NameToLayer("Obstacle"))
        {
            GameObject explosion = Instantiate(explosionFactory);
            explosion.transform.position = transform.position;
            Destroy(gameObject);  // 자신만 파괴
        }
    }
}
