using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5.0f;
    Vector3 direction;
    public GameObject explosionFactory;

    private void Start()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        direction = new Vector3(mousePosition.x-transform.position.x, mousePosition.y-transform.position.y, 0);
        if(ScoreManager.currentStage <= 5)
        {
            speed += ScoreManager.currentStage;

        }
        else
        {
            speed += 5;
        }
 
        transform.up = direction;
    }
    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        int targetLayer = collision.gameObject.layer;

        if (targetLayer == LayerMask.NameToLayer("Player") ||
            targetLayer == LayerMask.NameToLayer("Enemy") ||
            targetLayer == LayerMask.NameToLayer("Obstacle"))
        {
            GameObject explosion = Instantiate(explosionFactory);
            explosion.transform.position = transform.position;
            explosion.transform.localScale = transform.localScale;
            Destroy(gameObject);  // ÀÚ½Å¸¸ ÆÄ±«
        }
    }
}
