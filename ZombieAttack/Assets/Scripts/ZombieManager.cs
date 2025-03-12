using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    PlayerManager playerManager;
    Animator playerAnimator;

    public float HP = 100.0f;

    private void Start()
    {

    }
    private void Update()
    {
        CheckAlive();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
    private void OnTriggerEnter(Collider other)
    {
        playerManager = other.GetComponent<PlayerManager>();
        playerAnimator = other.GetComponent<Animator>();
        //플레이어 충돌시 사운드 재생
        if (playerManager != null)
        {
            playerManager.audioSource.PlayOneShot(playerManager.audioClipFire);
        }
        //플레이어 충돌시 피격 애니메이션 재생(레이어 우선순위 조절)
        if (playerAnimator != null)
        {
            playerAnimator.SetLayerWeight(1, 1);
            playerAnimator.SetTrigger("GettingHit");
            Invoke("ResetLayerWeight", 2.0f);
        }
        
        //플레이어 충돌시 특정 좌표로 이동
        if (other.gameObject.tag == "Player")
        {
            //player위치를 다루는 함수 비활성화
            playerManager.characterController.enabled = false;  
            other.gameObject.transform.position = new Vector3(0, 0, 0);
            playerManager.characterController.enabled = true;
        }
        else
        {
            other.GetComponent<MeshRenderer>().material.color = Color.red;
        }

    }
    void ResetLayerWeight()
    {
        playerAnimator.SetLayerWeight(1, 0);
    }
    void CheckAlive()
    {
        if(HP == 0)
        {
            Debug.Log("target destroyed");
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP < 0) HP = 0; // HP가 음수가 되지 않도록 제한
        Debug.Log("remain HP : " +  HP);
    }

}
