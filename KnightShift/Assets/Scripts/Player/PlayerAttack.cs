using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private PlayerAnimation playerAnimation;
    private PlayerAudio playerAudio;

    public GameObject attackHitbox;
    public GameObject blockHitbox;
    public float parryInvincibilityDuration = 5.0f; // 무적 시간

    public LayerMask enemyLayer;

    private bool isAttacking;

    //함수를 이용한 camera shake
    //float shakeDuration = 0.5f;
    //float shakeMagnitude = 0.1f;
    private Vector3 originalPos;

    //cinemachine의 impulse listener와 impulse source를 이용한 camera shake
    public CinemachineImpulseSource impulseSource;

    public float playerDamage = 20;

    private PlayerHealth playerHealth;
    public GameObject attackEffectPrefab; // 공격 이펙트 프리팹

    public GameObject shieldIcon;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerAudio = GetComponent<PlayerAudio>();

        attackHitbox.SetActive(false);
        blockHitbox.SetActive(false);
        shieldIcon.SetActive(false);
        playerHealth = GetComponent<PlayerHealth>();

    }
    public void Combat()
    {
        PerformAttack();
        PerformBlock();
    }
    public void PerformAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerHealth.isAlive)
        {
            if (!playerAnimation.isDoingAction)
            {
                playerAnimation.TriggerAttack();
                //playerAudio.PlaySwordSwing();
                SoundManager.Instance.PlaySFX(SFXType.SwordSwing);
            }
            else
            {
                return;
            }
            StartCoroutine(playerAnimation.ActionkCooldownByAimation());
            //StartCoroutine(Shake(shakeDuration, shakeMagnitude));
            GenerateCameraImpulse();
            Instantiate(attackEffectPrefab, attackHitbox.transform.position, Quaternion.identity);

        }
    }
    public void PerformBlock()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerHealth.isAlive)
        {
            if (!playerAnimation.isDoingAction && playerHealth.shieldCount > 0)
            {
                playerAnimation.TriggerBlock();
                StartCoroutine(playerAnimation.ActionkCooldownByAimation());
                blockHitbox.SetActive(false);
            }
        }
    }

    public void GiveDamage()
    {
        attackHitbox.SetActive(true);
    }

    public void StopGivingDamage()
    {
        attackHitbox.SetActive(false);
    }

    public void StartBlock()
    {
        blockHitbox.SetActive(true);
    }
    public void StopBlock()
    {
        blockHitbox.SetActive(false);
    }
    //public IEnumerator Shake(float duration, float magnitude)
    //{
    //    Camera.main.GetComponent<CinemachineBrain>().enabled = false;
    //    if (Camera.main == null)
    //    {
    //        yield break;
    //    }

    //    float elapsed = 0.0f;

    //    while (elapsed < duration)
    //    {
    //        float x = Random.Range(-1f, 1f) * magnitude;
    //        float y = Random.Range(-1f, 1f) * magnitude;

    //        Camera.main.transform.localPosition
    //            = new Vector3(Camera.main.transform.localPosition.x + x, originalPos.y + y, -10);

    //        elapsed += Time.deltaTime;

    //        yield return null;
    //    }

    //    Camera.main.transform.localPosition = originalPos;
    //    Camera.main.GetComponent<CinemachineBrain>().enabled = true;
    //}

    private void GenerateCameraImpulse()
    {
        if(impulseSource != null)
        {
            Debug.Log("카메라 임펄스 발생");
            impulseSource.GenerateImpulse();
        }
        else
        {
            Debug.LogWarningFormat("ImpulseSource가 연결되어 있지 않습니다");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerHealth.isAlive) return;

        if (blockHitbox.activeSelf && (other.gameObject.layer == LayerMask.NameToLayer("SlimeAttack") || other.gameObject.layer == LayerMask.NameToLayer("DeathbringerAttack")))
        {
            playerHealth.shieldCount--;
            GenerateCameraImpulse();
            // 무적 처리
            StartCoroutine(TemporaryInvincibility(parryInvincibilityDuration));

            // 적 애니메이터에 Blocked 트리거 발동
            Animator enemyAnimator = other.GetComponentInParent<Animator>();
            if (enemyAnimator != null)
            {
                enemyAnimator.SetTrigger("Blocked");
            }

            // 효과음
            SoundManager.Instance.PlaySFX(SFXType.Blocked);
        }
    }
    IEnumerator TemporaryInvincibility(float duration)
    {
        playerHealth.isInvincible = true;
        shieldIcon.SetActive(true);
        Debug.Log("invincible enabled");
        yield return new WaitForSeconds(duration);
        playerHealth.isInvincible = false;
        shieldIcon.SetActive(false);
        Debug.Log("invincible disabled");
    }

}
