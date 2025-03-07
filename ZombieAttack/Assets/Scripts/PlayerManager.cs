using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float mouseSensitivity = 100.0f;
    public Transform cameraTransform;
    public CharacterController characterController;
    public Transform playerHead;    //1��Ī ����
    public float thirdPersonDistance = 3.0f;    //3��Ī ��� �÷��̾�/ī�޶� �Ÿ�
    public Vector3 thirdPersonOffset = new Vector3(0, 1.5f, 0);    //3��Ī ��� ī�޶� ������
    public Transform playerLookObj; //�÷��̾� �þ� ��ġ
    public float zoomDistance = 1.0f;  //3��Ī ��� ī�޶� Ȯ��� �Ÿ�
    public float zoomSpeed = 5.0f;     //3��Ī ��� ī�޶� Ȯ�� �ӵ�
    public float defaultFOV = 60.0f;
    public float zoomFov = 30.0f;

    private float currentDistance;
    private float targetDistance;
    private float targetFOV;
    private bool isZoomed = false;    //Ȯ�� ����
    private Coroutine zoomCoroutine;    //�ڷ�ƾ�� ����Ͽ� �� Ȯ��/��� ó��
    private Camera mainCamera;

    private float pitch = 0.0f;     //���� ȸ����(x, y, z�� ���� �򰥸� �� �ִ�)
    private float yaw = 0.0f;
    private bool isFirstPerson = false;
    private bool isCameraRotationSeperated = true;  //ī�޶� ȸ���� �÷��̾� ȸ���� ����������(1��Ī �������� ��Ȱ��ȭ)

    //�߷� ���� ����
    public float gravity = -9.81f;  //�÷��̾ ���� �� ������ �� �ۿ��ϴ� �߷�
    public float jumpHeight = 2.0f;
    private Vector3 velocity;
    private bool isGround;

    private Animator animator;
    private float horizontal;
    private float vertical;
    private bool isRunning = false;
    private float waklSpeed = 5.0f;
    private float runSpeed = 10.0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentDistance = thirdPersonDistance;
        targetDistance = thirdPersonDistance;
        targetFOV = defaultFOV;
        mainCamera = cameraTransform.GetComponent<Camera>();
        mainCamera.fieldOfView = defaultFOV;
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        //���콺 �Է��� �޾� ī�޶�� �÷��̾� ȸ�� ó��
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -45, 45);    //3��Ī ī�޶� ȸ���� pitch�� ����

        isGround = characterController.isGrounded;

        //���� �پ� �ְ� �߶��� ���� �ӵ� -> �ٴڿ� �÷��̾ ����� �� �������� �ӵ�
        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            isFirstPerson = !isFirstPerson;
            Debug.Log(isFirstPerson ? "1��Ī ���" : "3��Ī ���");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            isCameraRotationSeperated = !isCameraRotationSeperated;
            Debug.Log(isCameraRotationSeperated ? "ī�޶� �÷��̾�� ������ ȸ���մϴ�" : "ī�޶� ���� �÷��̾ ȸ���մϴ�");
        }

        //������
        if (isFirstPerson)
        {
            FirstPersonMovement();
        }
        else
        {
            ThirdPersonMovement();
        }

        //�� ��
        if (Input.GetMouseButtonDown(1))    //��Ŭ���� ���� �� �ٿ� 
        {
            if(zoomCoroutine != null)       //���� �Ǿ� �ִٸ� �Է� �� ���� ������
            {
                StopCoroutine(zoomCoroutine);
            }
            if (isFirstPerson)              //1��Ī ��(�þ߰� ������)
            {
                SetTargetFOV(zoomFov);
                zoomCoroutine = StartCoroutine(ZoomFieldOfView(targetFOV));
            }
            else                            //3��Ī ��(ī�޶� ����)
            {
                SetTargetDistance(zoomDistance);
                zoomCoroutine = StartCoroutine(ZoomCamera(targetDistance));
            }
        }

        if (Input.GetMouseButtonUp(1))      //��Ŭ���� ������ �� ����
        {
            if (zoomCoroutine != null)      
            {
                StopCoroutine(zoomCoroutine);
            }
            if (isFirstPerson)              
            {
                SetTargetFOV(defaultFOV);
                zoomCoroutine = StartCoroutine(ZoomFieldOfView(defaultFOV));
            }
            else                            
            {
                SetTargetDistance(thirdPersonDistance);
                zoomCoroutine = StartCoroutine(ZoomCamera(thirdPersonDistance));
            }
        }

        //�̵� �ִϸ��̼� ����
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        animator.SetBool("isRunning", isRunning);
        moveSpeed = isRunning ? runSpeed : waklSpeed;

    }

    //1��Ī ���������� ������ -> ĳ���� �����Ӱ� ī�޶� ���� �¹���(isCameraRotationSeperated�� �������� ����)
    void FirstPersonMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
        moveDirection.y = 0;
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        cameraTransform.position = playerHead.position;
        cameraTransform.rotation = Quaternion.Euler(pitch, yaw, 0);

        transform.rotation = Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0);
    }

    //3��Ī ���������� ������ -> ĳ���Ͱ� ���� �����̰� ī�޶� ���Ѿư�(������ �ʿ�)
    void ThirdPersonMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        if (isCameraRotationSeperated)
        {
            //ī�޶� �÷��̾� �����ʿ��� ȸ���ϵ��� ����
            Vector3 direction = new Vector3(0, 0, -currentDistance);
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

            //ī�޶� �÷��̾��� �����ʿ��� ������ ��ġ�� �̵�
            cameraTransform.position = transform.position + thirdPersonOffset + rotation * direction;

            //ī�޶� �÷��̾� ��ġ�� ���󰡵��� ����
            cameraTransform.LookAt(transform.position + new Vector3(0, thirdPersonOffset.y, 0));
        }
        else
        {
            //�÷��̾ ���� ȸ�� 
            transform.rotation = Quaternion.Euler(-0f, yaw, 0);
            Vector3 direction = new Vector3(0, 0, -currentDistance);
            cameraTransform.position = playerLookObj.position + thirdPersonOffset
                + Quaternion.Euler(pitch, yaw, 0) * direction;
            cameraTransform.LookAt(playerLookObj.position + new Vector3(0, thirdPersonOffset.y, 0));
        }
    }

    public void SetTargetDistance(float distance)
    {
        targetDistance = distance;
    }

    public void SetTargetFOV(float FOV)
    {
        targetFOV = FOV;
    }


    //3��Ī/1��Ī �� ���
    //�Լ��� ���� ����� ���Ͱ� ��
    IEnumerator ZoomCamera(float targetDistance)    //3��Ī ��(ī�޶� �����̱�)
    {
        while (Mathf.Abs(currentDistance - targetDistance) > 0.01f)  //���� �Ÿ� -> ��ǥ �Ÿ��� �ε巴�� �̵�
        {
            currentDistance = Mathf.Lerp(currentDistance, targetDistance, zoomSpeed * Time.deltaTime);
            yield return null;
        }
        currentDistance = targetDistance;       //��ǥ �Ÿ��� ������ �� ���� ����
    }
    IEnumerator ZoomFieldOfView(float targetFOV)    //1��Ī ��(�þ߰� ������)
    {
        while (Mathf.Abs(mainCamera.fieldOfView - targetFOV) > 0.01f)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
            yield return null;
        }
        mainCamera.fieldOfView = targetFOV;     //��ǥ �þ߰��� ������ �� ���� ����
    }
}
