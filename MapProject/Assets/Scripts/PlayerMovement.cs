using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    public float speed = 5f; // �̵� �ӵ�
    private Rigidbody rb;    // Rigidbody ������Ʈ ����

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody ��������

        // Rigidbody�� ������ �ڵ� �߰�
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.freezeRotation = true; // ȸ�� ���� (�Ѿ����� �� ����)
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Vertical"); // W(+1) S(-1)
        float moveZ = Input.GetAxis("Horizontal");   // A(-1) D(+1)

        Vector3 move = new Vector3(moveX, 0, -moveZ) * speed; // �̵� ���� ����
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z); // �߷� ����
    }


}
