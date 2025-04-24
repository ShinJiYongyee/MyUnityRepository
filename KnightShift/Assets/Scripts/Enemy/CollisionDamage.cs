using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public float damage = 20f;
    public float damageInterval = 1f; // ���� ���� (��)
    public bool isActive = true;

    // ���� ��ü�� ���� �ٸ� ���� ���͹��� �����ϱ� ���� dictionary<�浹ü, ������ �浹 ����> ���
    private Dictionary<Collider2D, float> lastDamageTime = new Dictionary<Collider2D, float>();

    // �浹�� �����ϴ� ���� �÷��̾�� �������� �ִ� �Լ�
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(gameObject.name + " collided with " + collision.name);
        if (collision.CompareTag("Player") && isActive)
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // ó�� �浹�� ��� ��ųʸ��� �ݶ��̴� ���
                if (!lastDamageTime.ContainsKey(collision))
                {
                    lastDamageTime[collision] = Time.time;
                    playerHealth.GetDamage(damage);
                }

                // �ʱ� �ð��� ����
                float timeSinceLastDamage = Time.time - lastDamageTime[collision];
                // ���� �ð��� ������ ���� �ð��� ���� ���, �浹 �� �ð��� ���͹��� �Ѿ��� ���
                if (timeSinceLastDamage >= damageInterval)
                {
                    // ���� ��� �� ������ ���� �ð� ������Ʈ
                    playerHealth.GetDamage(damage);
                    lastDamageTime[collision] = Time.time;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (lastDamageTime.ContainsKey(collision))
        {
            lastDamageTime.Remove(collision); // �浹 ���� �� ��ųʸ� ����
        }
    }

}
