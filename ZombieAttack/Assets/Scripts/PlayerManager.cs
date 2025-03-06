using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float mouseSensitivity = 100.0f;
    public Transform cameraTransform;
    public CharacterController characterController;
    public Transform playerHead;    //1��Ī ����
    public float thirdPersonDistance = 3.0f;    //3��Ī ��� �÷��̾�/ī�޶� �Ÿ�
    public Vector3 thirdPersonOffset = new Vector3 (0, 1.5f, 0);    //3��Ī ��� ī�޶� ������
    public Transform playerLookObj; //�÷��̾� �þ� ��ġ
    public float zoomDistance = 1.0f;  //3��Ī ��� ī�޶� Ȯ��� �Ÿ�
    public float zoomSpeed = 5.0f;     //3��Ī ��� ī�޶� Ȯ�� �ӵ�
    public float defaultFov = 60.0f;
    public float zoomFov = 30.0f;

    private float currentDistance;
    private float targetDistance;
    private float targetFOV;
    private bool isZoomed=false;    //Ȯ�� ����
    private Coroutine zoomCoroutine;    //�ڷ�ƾ�� ����Ͽ� Ȯ��/��� ó��
    private Camera mainCamera;

    private float pitch = 0.0f;     //���� ȸ����(x, y, z�� ���� �򰥸� �� �ִ�)
    private float yaw = 0.0f;
    private bool isFirstPerson = false;
    private bool rotateAroundPlayer = true;

    //�߷� ���� ����
    public float gravity = -9.81f;
    public float jumpHeight = 2.0f;
    private Vector3 velocity;
    private bool isGround;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
