using UnityEngine;

public class ImpactFuse : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null) // �浹�� ������Ʈ�� �����ϴ��� Ȯ��
        {
            Destroy(other.gameObject); // �浹�� ������Ʈ ����
            Destroy(gameObject); // �ڽŵ� ����
        }
    }
}
