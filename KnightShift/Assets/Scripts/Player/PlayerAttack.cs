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

    public LayerMask enemyLayer;

    private bool isAttacking;

    //�Լ��� �̿��� camera shake
    //float shakeDuration = 0.5f;
    //float shakeMagnitude = 0.1f;
    private Vector3 originalPos;

    //cinemachine�� impulse listener�� impulse source�� �̿��� camera shake
    public CinemachineImpulseSource impulseSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerAudio = GetComponent<PlayerAudio>();

        attackHitbox.SetActive(false);

    }

    public void PerformAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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

    public void GiveDamage()
    {
        attackHitbox.SetActive(true);
    }

    public void StopGivingDamage()
    {
        attackHitbox.SetActive(false);
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Camera.main.GetComponent<CinemachineBrain>().enabled = false;
        if (Camera.main == null)
        {
            yield break;
        }

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.localPosition
                = new Vector3(Camera.main.transform.localPosition.x + x, originalPos.y + y, -10);

            elapsed += Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.localPosition = originalPos;
        Camera.main.GetComponent<CinemachineBrain>().enabled = true;
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
}
