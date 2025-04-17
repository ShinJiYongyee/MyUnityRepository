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
    public float parryInvincibilityDuration = 5.0f; // ���� �ð�

    public LayerMask enemyLayer;

    private bool isAttacking;

    //�Լ��� �̿��� camera shake
    //float shakeDuration = 0.5f;
    //float shakeMagnitude = 0.1f;
    private Vector3 originalPos;

    //cinemachine�� impulse listener�� impulse source�� �̿��� camera shake
    public CinemachineImpulseSource impulseSource;

    public float playerDamage = 20;

    private PlayerHealth playerHealth;
    public GameObject attackEffectPrefab; // ���� ����Ʈ ������
    public GameObject blockEffectPrefab;

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

        }
    }
    public void PerformBlock()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerHealth.isAlive && !playerHealth.isInvincible)
        {
            if (!playerAnimation.isDoingAction && playerHealth.shieldCount > 0)
            {
                playerAnimation.TriggerBlock();
                StartCoroutine(playerAnimation.ActionkCooldownByAimation());
            }
        }
    }

    // �ִϸ��̼ǿ��� ȣ��
    public void GiveDamage()
    {
        // ��Ʈ�ڽ� Ȱ��ȭ
        attackHitbox.SetActive(true);
        
        // ����Ʈ ����(���, ��ġ, ȸ��, �θ�)
        GameObject effect = Instantiate(attackEffectPrefab, attackHitbox.transform.position, Quaternion.identity, transform);
        
        // ����Ʈ ũ�� ����
        effect.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // ũ�� ���� (�ʿ信 ���� ����)

        // ����Ʈ�� ������ �������� �տ� ���̵��� sorting layer ����
        ParticleSystemRenderer[] renderers = effect.GetComponentsInChildren<ParticleSystemRenderer>();
        foreach (var r in renderers)
        {
            r.sortingLayerName = "Foreground";
            r.sortingOrder = 10;
        }

        // ����Ʈ ��� �ð��� ParticleSystem ������� ������ ���� �� �ڵ� ����
        ParticleSystem particleSystem = effect.GetComponent<ParticleSystem>();
        if(particleSystem != null)
        {
            Destroy(effect, particleSystem.main.duration);
        }
    }

    public void StopGivingDamage()
    {
        attackHitbox.SetActive(false);
    }

    public void StartBlock()
    {

        blockHitbox.SetActive(true);

        GameObject effect = Instantiate(blockEffectPrefab, transform.position, Quaternion.identity, transform);

        effect.transform.localScale = new Vector3(0.18f, 0.18f, 0.18f);

        ParticleSystemRenderer[] renderers = effect.GetComponentsInChildren<ParticleSystemRenderer>();
        foreach (var r in renderers)
        {
            r.sortingLayerName = "Foreground";
            r.sortingOrder = 10;
        }

        ParticleSystem particleSystem = effect.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            Destroy(effect, particleSystem.main.duration);
        }
        Debug.Log("Block start");
    }
    public void StopBlock()
    {
        blockHitbox.SetActive(false);
        Debug.Log("Block stop");
    }

    private void GenerateCameraImpulse()
    {
        if(impulseSource != null)
        {
            Debug.Log("ī�޶� ���޽� �߻�");
            impulseSource.GenerateImpulse();
        }
        else
        {
            Debug.LogWarningFormat("ImpulseSource�� ����Ǿ� ���� �ʽ��ϴ�");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerHealth.isAlive) return;

        if (blockHitbox.activeSelf && 
            (other.gameObject.layer == LayerMask.NameToLayer("SlimeAttack") || 
            other.gameObject.layer == LayerMask.NameToLayer("DeathbringerAttack")))
        {
            if(playerHealth.shieldCount > 0) playerHealth.shieldCount--;
            GenerateCameraImpulse();
            // ���� ó��
            StartCoroutine(TemporaryInvincibility(parryInvincibilityDuration));

            // �� �ִϸ����Ϳ� Blocked Ʈ���� �ߵ�
            Animator enemyAnimator = other.GetComponentInParent<Animator>();
            if (enemyAnimator != null)
            {
                enemyAnimator.SetTrigger("Blocked");
            }

            // ȿ����
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
