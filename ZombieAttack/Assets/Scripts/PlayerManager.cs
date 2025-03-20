using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

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
    //private bool isZoomed = false;    //확대 여부
    private Coroutine zoomCoroutine;    //코루틴을 사용하여 줌 확대/축소 처리
    private Camera mainCamera;

    private float pitch = 0.0f;     //상하 회전값(x, y, z를 쓰면 헷갈릴 수 있다)
    private float yaw = 0.0f;
    private bool isFirstPerson = false;
    private bool isCameraRotationSeperated = false;  //카메라 회전이 플레이어 회전과 독립적인지(1인칭 시점에서 비활성화)

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
    public AudioSource audioSource;

    public GameObject RifleM4;

    public Transform aimTarget;

    private float weaponMaxDistance = 100.0f;
    private float weaponDamage = 30.0f;

    private int animationspeed = 1;

    public MultiAimConstraint multiAimConstraint;
    public LayerMask TargetLayerMask;

    //아이템 줍기 애니메이션 변수
    private bool isGettingItem = false; // 아이템 줍기 중인지 여부
    private bool isHoldingRifle = false;

    //아이템 줍기 동작 변수
    public Vector3 boxSize = new Vector3(1.0f, 1.0f, 1.0f);
    public float castDistance = 5.0f;
    public LayerMask itemLayer;
    public Transform itemGetPos;

    //UI 아이콘 제어 변수
    public GameObject crosshairObj;
    public GameObject weaponIconObj;
    public bool hasM4Item;

    //총구 화염 구현 변수
    public ParticleSystem M4Effect;

    //사격 간 딜레이
    private float rifleFireDelay = 0.3f;

    bool isPlayingAnimation = false; // 애니메이션 진행 중 여부

    private float muzzleFlashDuration = 0.1f; //총구 화염 수명

    //착탄 이펙트와 효과음
    public ParticleSystem damageParticleSystem;
    public AudioClip audioClipDamage;

    //잔탄 표시 UI
    public Text bulletText;
    private int loadedBullet = 30;
    private int totalBullet = 0;
    private int magSize = 30;

    public GameObject flashLightObj;
    private bool isFlashLightOn = false;
    public AudioClip flashLightSound;

    public float playerHP = 100.0f;

    public GameObject pauseObj;
    private bool isPaused = false;

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
        crosshairObj.SetActive(false);
        weaponIconObj.SetActive(false);
        bulletText.gameObject.SetActive(false);
        flashLightObj.SetActive(false);

        hasM4Item = false;

        pauseObj.SetActive(isPaused);
    }

    void Update()
    {

        RotateCamera();
        StickOnGround();
        SwitchPerspective();
        SwitchCameraRotationSeperated();
        if (!isGettingItem)
        {
            SetMovement();
            Aim();
            Fire();
            Run();
            GetItem();
            Reload();
        }
        ActionFlashLight();
        SelectWeapon();
        SetMovingAnimation();

        animator.speed = animationspeed;    //애니메이션 재생 속도 설정 및 저장

        bulletText.text = $"{loadedBullet}/{totalBullet}";

        PauseGame();

    }
    void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pauseObj.SetActive(isPaused);
        }
        if(isPaused)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1.0f; 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void ActionFlashLight()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            isFlashLightOn = !isFlashLightOn;
            flashLightObj.SetActive(isFlashLightOn);
            audioSource.PlayOneShot(flashLightSound);
        }
    }
    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 현재 장탄이 magSize 미만이어야만 장전 가능
            if (loadedBullet < magSize && totalBullet > 0)
            {
                int loadingBullet = 0;
                int neededBullet = magSize - loadedBullet; // 필요한 총알 수

                if (totalBullet >= neededBullet)
                {
                    totalBullet -= neededBullet;
                    loadedBullet += neededBullet;
                }
                else
                {
                    loadedBullet += totalBullet;
                    totalBullet = 0;
                }

                Debug.Log($"Reloaded! Loaded: {loadedBullet}, Remaining: {totalBullet}");
            }
            else
            {
                Debug.Log("Cannot reload: Magazine full or no bullets left.");
            }
        }
    }


    //1인칭 시점에서의 움직임 -> 캐릭터 움직임과 카메라가 직접 맞물림(isCameraRotationSeperated가 개입하지 않음)
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

    //3인칭 시점에서의 움직임 -> 캐릭터가 먼저 움직이고 카메라가 뒤쫓아감(딜레이 필요)
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

            UpdateAimTarget();

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

        while (Mathf.Abs(currentDistance - targetDistance) > 0.01f)  //현재 거리 -> 목표 거리로 부드럽게 이동
        {
            currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * zoomSpeed);
            yield return null;
        }
        currentDistance = targetDistance;       //목표 거리에 도달한 후 값을 고정

    }
    IEnumerator ZoomFieldOfView(float targetFOV)    //1인칭 줌(시야각 좁히기)
    {

        while (Mathf.Abs(mainCamera.fieldOfView - targetFOV) > 0.01f)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
            yield return null;
        }
        mainCamera.fieldOfView = targetFOV;     //목표 시야각에 도달한 후 값을 고정

    }

    void RotateCamera()
    {
        //마우스 입력을 받아 카메라와 플레이어 회전 처리
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -45, 45);    //3인칭 카메라 회전의 pitch각 제한
    }

    void StickOnGround()
    {
        isGround = characterController.isGrounded;

        //땅에 붙어 있고 추락할 때의 속도 -> 바닥에 플레이어가 닿았을 때 떨어지는 속도
        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void SwitchPerspective()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFirstPerson = !isFirstPerson;
            //(isFirstPerson ? "1인칭 모드" : "3인칭 모드");
        }
    }

    void SwitchCameraRotationSeperated()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isCameraRotationSeperated = !isCameraRotationSeperated;
            //(isCameraRotationSeperated ? "카메라가 플레이어와 별도로 회전합니다" : "카메라를 따라 플레이어가 회전합니다");
        }
    }
    void SetMovement()
    {
        //움직임
        if (isFirstPerson)
        {
            FirstPersonMovement();
        }
        else
        {
            ThirdPersonMovement();
        }
    }
    void Aim()
    {
        if (!isCameraRotationSeperated)
        {
            if (RifleM4.gameObject.activeSelf)
            {
                //줌 인
                if (Input.GetMouseButtonDown(1))    //우클릭을 눌러 줌 다운 
                {
                    isAim = true;
                    crosshairObj.SetActive(true);
                    //조준 시 자동으로 조준 지점 이동
                    multiAimConstraint.data.offset = new Vector3(-30, 0, 0);
                    //animator.SetBool("isAim", isAim);
                    animator.SetLayerWeight(1, 1);
                    if (zoomCoroutine != null)       //줌이 되어 있다면 입력 시 줌을 끝낸다
                    {
                        StopCoroutine(zoomCoroutine);
                    }
                    if (isFirstPerson)              //1인칭 줌(시야각 좁히기)
                    {
                        //("RMB pressed");

                        SetTargetFOV(zoomFOV);
                        zoomCoroutine = StartCoroutine(ZoomFieldOfView(targetFOV));
                    }
                    else                            //3인칭 줌(카메라 당기기)
                    {
                        //("RMB pressed");

                        SetTargetDistance(zoomDistance);
                        zoomCoroutine = StartCoroutine(ZoomCamera(targetDistance));

                    }
                }
                //줌 아웃
                if (Input.GetMouseButtonUp(1))      //우클릭을 해제해 줌 해제
                {
                    isAim = false;
                    crosshairObj.SetActive(false);
                    //animator.SetBool("isAim", isAim);
                    animator.SetLayerWeight(1, 0);
                    if (zoomCoroutine != null)
                    {
                        StopCoroutine(zoomCoroutine);
                    }
                    if (isFirstPerson)
                    {
                        //("RMB released");

                        SetTargetFOV(defaultFOV);
                        zoomCoroutine = StartCoroutine(ZoomFieldOfView(targetFOV));    //defaultFOV -> targetFOV
                    }
                    else
                    {
                        //("RMB released");

                        SetTargetDistance(thirdPersonDistance);
                        zoomCoroutine = StartCoroutine(ZoomCamera(targetDistance)); //thirdPersonDistance -> targetDistance
                    }
                }

            }
        }
    }
    void Fire()
    {

        //조준 사격
        if (Input.GetMouseButtonDown(0))
        {
            if (isAim && !isFireing)
            {
                //("fire");

                if (loadedBullet > 0)
                {
                    loadedBullet--;
                    bulletText.text = $"{loadedBullet}/{totalBullet}";
                    bulletText.gameObject.SetActive(true);

                }
                else
                {
                    //재장전?
                    //잔탄 고갈 효과음
                    return;
                }

                //사격 애니메이션 재생 및 사격음 출력
                weaponMaxDistance = 1000.0f;
                isFireing = true;
                animator.SetTrigger("Fire");
                audioSource.PlayOneShot(audioClipFire);
                M4Effect.Play();
                StartCoroutine(StopMuzzleFlash(muzzleFlashDuration)); //총구 화염 끄기


                //fire delay data fix
                StartCoroutine(FireDelay(rifleFireDelay));

                //사격 시 가상의 광선을 발사
                Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
                RaycastHit[] hits = Physics.RaycastAll(ray, weaponMaxDistance, TargetLayerMask);
                if (hits.Length > 0)
                {
                    for (int i = 0; i < hits.Length && i < 2; i++)
                    {
                        Debug.Log("충돌 : " + hits[i].collider.gameObject.name);

                        ParticleSystem particle = Instantiate(damageParticleSystem, hits[i].point, Quaternion.identity);
                        damageParticleSystem.transform.position = hits[i].point;
                        damageParticleSystem.Play();
                        audioSource.PlayOneShot(audioClipDamage);

                        // 맞은 오브젝트가 Zombie라면 HP 감소
                        ZombieManager zombie = hits[i].collider.GetComponent<ZombieManager>();
                        if (zombie != null)
                        {
                           zombie.StartCoroutine("TakeDamage", weaponDamage);

                        }

                        Debug.DrawLine(ray.origin, hits[i].point, Color.red, 2.0f);
                    }
                }
                else
                {
                    Debug.DrawLine(ray.origin, ray.origin + ray.direction * weaponMaxDistance, Color.green, 2.0f);
                }

            }
        }

    }
    IEnumerator FireDelay(float fireDelay)
    {

        yield return new WaitForSeconds(fireDelay);
        isFireing = false;
    }
    // 총구 화염 끄는 코루틴 추가
    IEnumerator StopMuzzleFlash(float delay)
    {
        yield return new WaitForSeconds(delay);
        M4Effect.Stop();
    }

    void Run()
    {
        //달리기
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    void SelectWeapon()
    {
        if (hasM4Item)
        {
            //무기 선택
            //총 꺼내기/넣기 토글
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                isHoldingRifle = !isHoldingRifle;
                RifleM4.SetActive(isHoldingRifle);
                bulletText.gameObject.SetActive(isHoldingRifle);

                //꺼낼 때 애니메이션 재생
                if (isHoldingRifle && !isPlayingAnimation)
                {
                    isPlayingAnimation = true;
                    //animator.SetTrigger("RiflePullOut");
                    StartCoroutine(HandleSelectWeapon());
                }
            }
        }

    }
    IEnumerator HandleSelectWeapon()
    {
        animator.SetLayerWeight(1, 1);
        animator.SetTrigger("isWeaponChange");
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(1);
        while (!animatorStateInfo.IsName("isWeaponChange"))
        {

            yield return null; // 다음 프레임까지 대기
            animatorStateInfo = animator.GetCurrentAnimatorStateInfo(1); // 상태 갱신
        }
        float animationLength = animatorStateInfo.length;

        yield return new WaitForSeconds(animationLength);
        isPlayingAnimation = false;
        //animator.SetLayerWeight(1, 0);  //구동되지 않음
    }

    void SetMovingAnimation()
    {
        //이동 애니메이션 제어
        moveSpeed = isRunning ? runSpeed : waklSpeed;
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetBool("isRunning", isRunning);
    }

    public void PlayWeaponChangeSound()
    {
        audioSource.PlayOneShot(audioClipEquip);
    }


    void UpdateAimTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        aimTarget.position = ray.GetPoint(10.0f);
    }

    //아이템 줍기
    void GetItem()
    {
        isHoldingRifle = RifleM4.activeSelf;

        if (Input.GetKeyDown(KeyCode.E) && !isGettingItem) // E를 처음 눌렀을 때만
        {
            Vector3 origin = itemGetPos.position;
            Vector3 direction = itemGetPos.forward;
            RaycastHit[] hits = Physics.BoxCastAll(origin, boxSize / 2, direction, Quaternion.identity, castDistance, itemLayer);

            if (hits.Length > 0) // 주울 아이템이 있다면
            {
                if(totalBullet < 120)
                {
                    isGettingItem = true; // 줍기 상태로 변경
                    RifleM4.SetActive(false); // 무기 비활성화
                    animator.Play("PickUp", 0, 0f); // 애니메이션 재생
                    StartCoroutine(HandleItemPickup(hits)); // 감지된 아이템 전달
                    //bulletText.gameObject.SetActive(true);
                    totalBullet += 30;
                }
                else
                {
                    Debug.Log("탄을 더 이상 획득할 수 없습니다");
                }
            }
            else
            {
                Debug.Log("주울 수 있는 아이템이 없습니다."); // 아이템 없을 때
            }
        }
    }


    IEnumerator HandleItemPickup(RaycastHit[] hits)
    {
        yield return null; // 한 프레임 대기 (애니메이션 준비)

        // 애니메이션 길이 가져오기
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;
        //Debug.Log($"[아이템 줍기] 애니메이션 길이: {animationLength}");

        // 아이템 처리
        foreach (RaycastHit hit in hits)
        {
            hit.collider.gameObject.SetActive(false); // 아이템 비활성화
            Debug.Log("아이템 획득: " + hit.collider.gameObject.name);
            audioSource.PlayOneShot(audioClipEquip);
            hasM4Item = true;
            weaponIconObj.SetActive(true);
        }

        // 애니메이션 완료까지 대기
        yield return new WaitForSeconds(animationLength);

        //Debug.Log("[아이템 줍기] 완료");
        isGettingItem = false; // 줍기 완료
        animator.Play("Movement"); // 기본 동작 복귀
        RifleM4.SetActive(isHoldingRifle); // 이전 무기 상태 복구
    }



}
