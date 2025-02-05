using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    public float speed = 5f; // 이동 속도
    private Rigidbody rb;    // Rigidbody 컴포넌트 참조

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 가져오기

        // Rigidbody가 없으면 자동 추가
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.freezeRotation = true; // 회전 고정 (넘어지는 것 방지)
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Vertical"); // W(+1) S(-1)
        float moveZ = Input.GetAxis("Horizontal");   // A(-1) D(+1)

        Vector3 move = new Vector3(moveX, 0, -moveZ) * speed; // 이동 방향 설정
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z); // 중력 유지
    }


}
