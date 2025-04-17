using UnityEngine;

public class JumpPoint : MonoBehaviour
{
    public float jumpForce = 10f;

    // ������ ����� ���� ����
    public enum AllowedDirection { Both, LeftOnly, RightOnly }
    public AllowedDirection allowedDirection = AllowedDirection.Both;
}
