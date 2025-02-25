using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float scrollSpeed = 0.5f;     //background와 동일

    void Update()
    {
        transform.position += -transform.up.normalized * scrollSpeed * Time.deltaTime;
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        int targetLayer = collision.gameObject.layer;

        if (targetLayer == LayerMask.NameToLayer("Player") ||
            targetLayer == LayerMask.NameToLayer("Enemy") ||
            targetLayer == LayerMask.NameToLayer("Bullet"))
        {
            Destroy(collision.gameObject);  // 상대방만 파괴
        }
    }

}
