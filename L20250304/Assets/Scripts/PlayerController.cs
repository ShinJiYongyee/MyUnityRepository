using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float moveSpeed = 3.0f;
    float rollSpeed = 60.0f;
    float pitchSpeed = 60.0f;
    float yawSpeed = 60.0f;

    void Update()
    {
        float pitch = Input.GetAxisRaw("Vertical");
        float roll = Input.GetAxisRaw("Horizontal");
        float yaw = 0;

        if (Input.GetKey(KeyCode.E))
        {
            yaw++;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            yaw--;
        }

        // 전진 이동
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

        // 올바른 회전 방식 적용
        transform.Rotate(Vector3.right * pitch * Time.deltaTime * pitchSpeed, Space.Self); // Pitch
        transform.Rotate(Vector3.up * yaw * Time.deltaTime * yawSpeed, Space.Self);       // Yaw
        transform.Rotate(Vector3.forward * -roll * Time.deltaTime * rollSpeed, Space.Self); // Roll
    }
}
