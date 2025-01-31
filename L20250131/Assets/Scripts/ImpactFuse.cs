using UnityEngine;

public class ImpactFuse : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null) // 충돌한 오브젝트가 존재하는지 확인
        {
            Destroy(other.gameObject); // 충돌한 오브젝트 제거
            Destroy(gameObject); // 자신도 제거
        }
    }
}
