using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    public float speed = 5f; // 이동 속도
    public float mouseSensitivity = 5f; // 마우스 감도 추가

    private Rigidbody rb; // Rigidbody 컴포넌트 참조
    private Camera mainCamera; // 메인 카메라 참조

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 가져오기

        // Rigidbody가 없으면 자동 추가
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.freezeRotation = true; // 회전 고정 (넘어지는 것 방지)
        mainCamera = Camera.main; // 카메라 가져오기
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotateTowardsMouse();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal"); // A(-1) D(+1)
        float moveZ = Input.GetAxis("Vertical");   // W(+1) S(-1)

        Vector3 move = new Vector3(moveX, 0, moveZ) * speed; // 이동 방향 설정
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z); // 중력 유지
    }

    void RotateTowardsMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 lookPos = hit.point;

            // 현재 회전
            Quaternion currentRotation = transform.rotation;

            // 목표 회전
            Quaternion targetRotation = Quaternion.LookRotation(lookPos - transform.position);

        }
    }
}
