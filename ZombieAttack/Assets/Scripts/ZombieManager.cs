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
        //�÷��̾� �浹�� ���� ���
        if (playerManager != null)
        {
            playerManager.audioSource.PlayOneShot(playerManager.audioClipFire);
        }
        //�÷��̾� �浹�� �ǰ� �ִϸ��̼� ���(���̾� �켱���� ����)
        if (playerAnimator != null)
        {
            playerAnimator.SetLayerWeight(1, 1);
            playerAnimator.SetTrigger("GettingHit");
            Invoke("ResetLayerWeight", 2.0f);
        }
        
        //�÷��̾� �浹�� Ư�� ��ǥ�� �̵�
        if (other.gameObject.tag == "Player")
        {
            //player��ġ�� �ٷ�� �Լ� ��Ȱ��ȭ
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
        if (HP < 0) HP = 0; // HP�� ������ ���� �ʵ��� ����
        Debug.Log("remain HP : " +  HP);
    }

}
