using UnityEngine;

public class JumpPoint : MonoBehaviour
{
    public float jumpForce = 7f;
    public float horizontalForce = 2f;

    // ������ ����� ���� ����
    public enum AllowedDirection { Both, LeftOnly, RightOnly }
    public AllowedDirection allowedDirection = AllowedDirection.Both;
}
