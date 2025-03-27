using UnityEngine;

public class ChopperManager : MonoBehaviour
{
    public float descentSpeed = 5f; //하강 속도
    private bool isDescending = false;
    public AudioSource audioSource;
    public AudioClip chopperAudioClip;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isDescending)
        {
            transform.position += Vector3.down * descentSpeed * Time.deltaTime;
        }
    }

    public void StartDescent()
    {
        isDescending = true;
        audioSource.PlayOneShot(chopperAudioClip);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                playerManager.hasWon = true;
                Debug.Log("Player Escaped!");
                audioSource.Stop();
            }
        }
    }
}
