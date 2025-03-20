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
    public Transform playerHead;    //1��Ī ����
    public float thirdPersonDistance = 3.0f;    //3��Ī ��� �÷��̾�/ī�޶� �Ÿ�
    public Vector3 thirdPersonOffset = new Vector3(0, 1.5f, 0);    //3��Ī ��� ī�޶� ������
    public Transform playerLookObj; //�÷��̾� �þ� ��ġ
    public float zoomDistance = 1.0f;  //3��Ī ��� ī�޶� Ȯ��� �Ÿ�
    public float zoomSpeed = 5.0f;     //3��Ī ��� ī�޶� Ȯ�� �ӵ�
    public float defaultFOV = 60.0f;
    public float zoomFOV = 30.0f;

    private float currentDistance;
    private float targetDistance;
    private float targetFOV;
    //private bool isZoomed = false;    //Ȯ�� ����
    private Coroutine zoomCoroutine;    //�ڷ�ƾ�� ����Ͽ� �� Ȯ��/��� ó��
    private Camera mainCamera;

    private float pitch = 0.0f;     //���� ȸ����(x, y, z�� ���� �򰥸� �� �ִ�)
    private float yaw = 0.0f;
    private bool isFirstPerson = false;
    private bool isCameraRotationSeperated = false;  //ī�޶� ȸ���� �÷��̾� ȸ���� ����������(1��Ī �������� ��Ȱ��ȭ)

    //�߷� ���� ����
    public float gravity = -9.81f;  //�÷��̾ ���� �� ������ �� �ۿ��ϴ� �߷�
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

    //������ �ݱ� �ִϸ��̼� ����
    private bool isGettingItem = false; // ������ �ݱ� ������ ����
    private bool isHoldingRifle = false;

    //������ �ݱ� ���� ����
    public Vector3 boxSize = new Vector3(1.0f, 1.0f, 1.0f);
    public float castDistance = 5.0f;
    public LayerMask itemLayer;
    public Transform itemGetPos;

    //UI ������ ���� ����
    public GameObject crosshairObj;
    public GameObject weaponIconObj;
    public bool hasM4Item;

    //�ѱ� ȭ�� ���� ����
    public ParticleSystem M4Effect;

    //��� �� ������
    private float rifleFireDelay = 0.3f;

    bool isPlayingAnimation = false; // �ִϸ��̼� ���� �� ����

    private float muzzleFlashDuration = 0.1f; //�ѱ� ȭ�� ����

    //��ź ����Ʈ�� ȿ����
    public ParticleSystem damageParticleSystem;
    public AudioClip audioClipDamage;

    //��ź ǥ�� UI
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

        animator.speed = animationspeed;    //�ִϸ��̼� ��� �ӵ� ���� �� ����

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
            // ���� ��ź�� magSize �̸��̾�߸� ���� ����
            if (loadedBullet < magSize && totalBullet > 0)
            {
                int loadingBullet = 0;
                int neededBullet = magSize - loadedBullet; // �ʿ��� �Ѿ� ��

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


    //3��Ī/1��Ī �� ���
    //�Լ��� ���� ����� ���Ͱ� ��
    IEnumerator ZoomCamera(float targetDistance)    //3��Ī ��(ī�޶� �����̱�)
    {

        while (Mathf.Abs(currentDistance - targetDistance) > 0.01f)  //���� �Ÿ� -> ��ǥ �Ÿ��� �ε巴�� �̵�
        {
            currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * zoomSpeed);
            yield return null;
        }
        currentDistance = targetDistance;       //��ǥ �Ÿ��� ������ �� ���� ����

    }
    IEnumerator ZoomFieldOfView(float targetFOV)    //1��Ī ��(�þ߰� ������)
    {

        while (Mathf.Abs(mainCamera.fieldOfView - targetFOV) > 0.01f)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
            yield return null;
        }
        mainCamera.fieldOfView = targetFOV;     //��ǥ �þ߰��� ������ �� ���� ����

    }

    void RotateCamera()
    {
        //���콺 �Է��� �޾� ī�޶�� �÷��̾� ȸ�� ó��
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -45, 45);    //3��Ī ī�޶� ȸ���� pitch�� ����
    }

    void StickOnGround()
    {
        isGround = characterController.isGrounded;

        //���� �پ� �ְ� �߶��� ���� �ӵ� -> �ٴڿ� �÷��̾ ����� �� �������� �ӵ�
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
            //(isFirstPerson ? "1��Ī ���" : "3��Ī ���");
        }
    }

    void SwitchCameraRotationSeperated()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isCameraRotationSeperated = !isCameraRotationSeperated;
            //(isCameraRotationSeperated ? "ī�޶� �÷��̾�� ������ ȸ���մϴ�" : "ī�޶� ���� �÷��̾ ȸ���մϴ�");
        }
    }
    void SetMovement()
    {
        //������
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
                //�� ��
                if (Input.GetMouseButtonDown(1))    //��Ŭ���� ���� �� �ٿ� 
                {
                    isAim = true;
                    crosshairObj.SetActive(true);
                    //���� �� �ڵ����� ���� ���� �̵�
                    multiAimConstraint.data.offset = new Vector3(-30, 0, 0);
                    //animator.SetBool("isAim", isAim);
                    animator.SetLayerWeight(1, 1);
                    if (zoomCoroutine != null)       //���� �Ǿ� �ִٸ� �Է� �� ���� ������
                    {
                        StopCoroutine(zoomCoroutine);
                    }
                    if (isFirstPerson)              //1��Ī ��(�þ߰� ������)
                    {
                        //("RMB pressed");

                        SetTargetFOV(zoomFOV);
                        zoomCoroutine = StartCoroutine(ZoomFieldOfView(targetFOV));
                    }
                    else                            //3��Ī ��(ī�޶� ����)
                    {
                        //("RMB pressed");

                        SetTargetDistance(zoomDistance);
                        zoomCoroutine = StartCoroutine(ZoomCamera(targetDistance));

                    }
                }
                //�� �ƿ�
                if (Input.GetMouseButtonUp(1))      //��Ŭ���� ������ �� ����
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

        //���� ���
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
                    //������?
                    //��ź �� ȿ����
                    return;
                }

                //��� �ִϸ��̼� ��� �� ����� ���
                weaponMaxDistance = 1000.0f;
                isFireing = true;
                animator.SetTrigger("Fire");
                audioSource.PlayOneShot(audioClipFire);
                M4Effect.Play();
                StartCoroutine(StopMuzzleFlash(muzzleFlashDuration)); //�ѱ� ȭ�� ����


                //fire delay data fix
                StartCoroutine(FireDelay(rifleFireDelay));

                //��� �� ������ ������ �߻�
                Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
                RaycastHit[] hits = Physics.RaycastAll(ray, weaponMaxDistance, TargetLayerMask);
                if (hits.Length > 0)
                {
                    for (int i = 0; i < hits.Length && i < 2; i++)
                    {
                        Debug.Log("�浹 : " + hits[i].collider.gameObject.name);

                        ParticleSystem particle = Instantiate(damageParticleSystem, hits[i].point, Quaternion.identity);
                        damageParticleSystem.transform.position = hits[i].point;
                        damageParticleSystem.Play();
                        audioSource.PlayOneShot(audioClipDamage);

                        // ���� ������Ʈ�� Zombie��� HP ����
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
    // �ѱ� ȭ�� ���� �ڷ�ƾ �߰�
    IEnumerator StopMuzzleFlash(float delay)
    {
        yield return new WaitForSeconds(delay);
        M4Effect.Stop();
    }

    void Run()
    {
        //�޸���
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
            //���� ����
            //�� ������/�ֱ� ���
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                isHoldingRifle = !isHoldingRifle;
                RifleM4.SetActive(isHoldingRifle);
                bulletText.gameObject.SetActive(isHoldingRifle);

                //���� �� �ִϸ��̼� ���
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

            yield return null; // ���� �����ӱ��� ���
            animatorStateInfo = animator.GetCurrentAnimatorStateInfo(1); // ���� ����
        }
        float animationLength = animatorStateInfo.length;

        yield return new WaitForSeconds(animationLength);
        isPlayingAnimation = false;
        //animator.SetLayerWeight(1, 0);  //�������� ����
    }

    void SetMovingAnimation()
    {
        //�̵� �ִϸ��̼� ����
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

    //������ �ݱ�
    void GetItem()
    {
        isHoldingRifle = RifleM4.activeSelf;

        if (Input.GetKeyDown(KeyCode.E) && !isGettingItem) // E�� ó�� ������ ����
        {
            Vector3 origin = itemGetPos.position;
            Vector3 direction = itemGetPos.forward;
            RaycastHit[] hits = Physics.BoxCastAll(origin, boxSize / 2, direction, Quaternion.identity, castDistance, itemLayer);

            if (hits.Length > 0) // �ֿ� �������� �ִٸ�
            {
                if(totalBullet < 120)
                {
                    isGettingItem = true; // �ݱ� ���·� ����
                    RifleM4.SetActive(false); // ���� ��Ȱ��ȭ
                    animator.Play("PickUp", 0, 0f); // �ִϸ��̼� ���
                    StartCoroutine(HandleItemPickup(hits)); // ������ ������ ����
                    //bulletText.gameObject.SetActive(true);
                    totalBullet += 30;
                }
                else
                {
                    Debug.Log("ź�� �� �̻� ȹ���� �� �����ϴ�");
                }
            }
            else
            {
                Debug.Log("�ֿ� �� �ִ� �������� �����ϴ�."); // ������ ���� ��
            }
        }
    }


    IEnumerator HandleItemPickup(RaycastHit[] hits)
    {
        yield return null; // �� ������ ��� (�ִϸ��̼� �غ�)

        // �ִϸ��̼� ���� ��������
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;
        //Debug.Log($"[������ �ݱ�] �ִϸ��̼� ����: {animationLength}");

        // ������ ó��
        foreach (RaycastHit hit in hits)
        {
            hit.collider.gameObject.SetActive(false); // ������ ��Ȱ��ȭ
            Debug.Log("������ ȹ��: " + hit.collider.gameObject.name);
            audioSource.PlayOneShot(audioClipEquip);
            hasM4Item = true;
            weaponIconObj.SetActive(true);
        }

        // �ִϸ��̼� �Ϸ���� ���
        yield return new WaitForSeconds(animationLength);

        //Debug.Log("[������ �ݱ�] �Ϸ�");
        isGettingItem = false; // �ݱ� �Ϸ�
        animator.Play("Movement"); // �⺻ ���� ����
        RifleM4.SetActive(isHoldingRifle); // ���� ���� ���� ����
    }



}
