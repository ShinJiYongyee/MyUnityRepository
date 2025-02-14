using UnityEngine;

public class Player : MonoBehaviour
{

    void Start()
    {
        GameManager.Instance.ScorePlus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
