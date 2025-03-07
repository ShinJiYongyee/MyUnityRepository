using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float mouseSensitivity = 100.0f;
    public Transform cameraTransform;
    public CharacterController characterController;
    public Transform playerHead;    //1인칭 시점
    public float thirdPersonDistance = 3.0f;    //3인칭 모드 플레이어/카메라 거리
    public Vector3 thirdPersonOffset = new Vector3(0, 1.5f, 0);    //3인칭 모드 카메라 오프셋
    public Transform playerLookObj; //플레이어 시야 위치
    public float zoomDistance = 1.0f;  //3인칭 모드 카메라 확대시 거리
    public float zoomSpeed = 5.0f;     //3인칭 모드 카메라 확대 속도
    public float defaultFOV = 60.0f;
    public float zoomFOV = 30.0f;

    private float currentDistance;
    private float targetDistance;
    private float targetFOV;
    private bool isZoomed = false;    //확대 여부
    private Coroutine zoomCoroutine;    //코루틴을 사용하여 줌 확대/축소 처리
    private Camera mainCamera;

    private float pitch = 0.0f;     //상하 회전값(x, y, z를 쓰면 헷갈릴 수 있다)
    private float yaw = 0.0f;
    private bool isFirstPerson = false;
    private bool isCameraRotationSeperated = true;  //카메라 회전이 플레이어 회전과 독립적인지(1인칭 시점에서 비활성화)

    //중력 관련 변수
    public float gravity = -9.81f;  //플레이어가 점프 후 떨어질 때 작용하는 중력
    public float jumpHeight = 2.0f;
    private Vector3 velocity;
    private bool isGround;

    private Animator animator;
    private float horizontal;
    private float vertical;
    private bool isRunning = false;
    private bool isAim = false;
    private bool isFireing = false;
    private float waklSpeed = 5.0f;
    private float runSpeed = 10.0f;

    public AudioClip audioClipFire;
    public AudioClip audioClipEquip;
    private AudioSource audioSource;

    public GameObject RifleM4;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentDistance = thirdPersonDistance;
        targetDistance = thirdPersonDistance;
        targetFOV = defaultFOV;
        mainCamera = cameraTransform.GetComponent<Camera>();
        mainCamera.fieldOfView = defaultFOV;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        RifleM4.SetActive(false);

    }

    void Update()
    {
        //마우스 입력을 받아 카메라와 플레이어 회전 처리
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -45, 45);    //3인칭 카메라 회전의 pitch각 제한

        isGround = characterController.isGrounded;

        //땅에 붙어 있고 추락할 때의 속도 -> 바닥에 플레이어가 닿았을 때 떨어지는 속도
        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            isFirstPerson = !isFirstPerson;
            Debug.Log(isFirstPerson ? "1인칭 모드" : "3인칭 모드");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            isCameraRotationSeperated = !isCameraRotationSeperated;
            Debug.Log(isCameraRotationSeperated ? "카메라가 플레이어와 별도로 회전합니다" : "카메라를 따라 플레이어가 회전합니다");
        }

        //움직임
        if (isFirstPerson)
        {
            FirstPersonMovement();
        }
        else
        {
            ThirdPersonMovement();
        }

        //줌 인
        if (Input.GetMouseButtonDown(1))    //우클릭을 눌러 줌 다운 
        {
            isAim = true;
            animator.SetBool("isAim", isAim);
            if (zoomCoroutine != null)       //줌이 되어 있다면 입력 시 줌을 끝낸다
            {
                StopCoroutine(zoomCoroutine);
            }
            if (isFirstPerson)              //1인칭 줌(시야각 좁히기)
            {
                Debug.Log("RMB pressed");

                SetTargetFOV(zoomFOV);
                zoomCoroutine = StartCoroutine(ZoomFieldOfView(targetFOV));
            }
            else                            //3인칭 줌(카메라 당기기)
            {
                Debug.Log("RMB pressed");

                SetTargetDistance(zoomDistance);
                zoomCoroutine = StartCoroutine(ZoomCamera(targetDistance));

            }
        }
        //줌 아웃
        if (Input.GetMouseButtonUp(1))      //우클릭을 해제해 줌 해제
        {
            isAim = false;
            animator.SetBool("isAim", isAim);
            if (zoomCoroutine != null)
            {
                StopCoroutine(zoomCoroutine);
            }
            if (isFirstPerson)
            {
                Debug.Log("RMB released");

                SetTargetFOV(defaultFOV);
                zoomCoroutine = StartCoroutine(ZoomFieldOfView(targetFOV));    //defaultFOV -> targetFOV
            }
            else
            {
                Debug.Log("RMB released");

                SetTargetDistance(thirdPersonDistance);
                zoomCoroutine = StartCoroutine(ZoomCamera(targetDistance)); //thirdPersonDistance -> targetDistance
            }
        }


        //조준 사격
        if (Input.GetMouseButtonDown(0))
        {
            if (isAim)
            {
                Debug.Log("fire");

                isFireing = true;
                animator.SetBool("isFireing", isFireing);
                audioSource.PlayOneShot(audioClipFire);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isFireing = false;
            animator.SetBool("isFireing", isFireing);
        }

        //달리기
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        //무기 선택
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            audioSource.PlayOneShot(audioClipEquip);
            animator.SetTrigger("isWeaponChange");
            RifleM4.SetActive(true);
        }

        //이동 애니메이션 제어
        moveSpeed = isRunning ? runSpeed : waklSpeed;
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetBool("isRunning", isRunning);

    }

    //1인칭 시점에서의 움직임 -> 캐릭터 움직임과 카메라가 직접 맞물림(isCameraRotationSeperated가 개입하지 않음)
    void FirstPersonMovement()
    {
        if (!isAim)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            Vector3 moveDirection = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
            moveDirection.y = 0;
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        }
        cameraTransform.position = playerHead.position;
        cameraTransform.rotation = Quaternion.Euler(pitch, yaw, 0);

        transform.rotation = Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0);


    }

    //3인칭 시점에서의 움직임 -> 캐릭터가 먼저 움직이고 카메라가 뒤쫓아감(딜레이 필요)
    void ThirdPersonMovement()
    {
        if (!isAim)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            characterController.Move(move * moveSpeed * Time.deltaTime);

        }
        UpdateCameraPosition();

    }

    void UpdateCameraPosition()
    {
        if (isCameraRotationSeperated)
        {
            //카메라가 플레이어 오른쪽에서 회전하도록 설정
            Vector3 direction = new Vector3(0, 0, -currentDistance);
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

            //카메라를 플레이어의 오른쪽에서 고정된 위치로 이동
            cameraTransform.position = transform.position + thirdPersonOffset + rotation * direction;

            //카메라가 플레이어 위치를 따라가도록 설정
            cameraTransform.LookAt(transform.position + new Vector3(0, thirdPersonOffset.y, 0));
        }
        else
        {
            //플레이어가 직접 회전 
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


    //3인칭/1인칭 줌 기능
    //함수에 비해 실행과 복귀가 편리
    IEnumerator ZoomCamera(float targetDistance)    //3인칭 줌(카메라 움직이기)
    {
        Debug.Log("camera zoom called, distance : "+targetDistance);

        while (Mathf.Abs(currentDistance - targetDistance) > 0.01f)  //현재 거리 -> 목표 거리로 부드럽게 이동
        {
            currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * zoomSpeed);
            yield return null;
        }
        currentDistance = targetDistance;       //목표 거리에 도달한 후 값을 고정

    }
    IEnumerator ZoomFieldOfView(float targetFOV)    //1인칭 줌(시야각 좁히기)
    {
        Debug.Log("fov zoom called");

        while (Mathf.Abs(mainCamera.fieldOfView - targetFOV) > 0.01f)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
            yield return null;
        }
        mainCamera.fieldOfView = targetFOV;     //목표 시야각에 도달한 후 값을 고정

    }


}
