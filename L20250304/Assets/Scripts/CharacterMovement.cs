using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 4.0f;
    public float rotationSpeed = 360.0f;


    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        transform.Translate(Vector3.forward * v * Time.deltaTime * moveSpeed);
        transform.Rotate(Vector3.up * h * Time.deltaTime * rotationSpeed);
    }
}
