using UnityEngine;

public class ItemRotator : MonoBehaviour
{
    private void Start()
    {
        CheckForDuplicate();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime % 360 * 60, 0);
    }



    private void CheckForDuplicate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (Collider col in colliders)
        {
            if (col.gameObject != gameObject && col.gameObject.layer == LayerMask.NameToLayer("Item"))
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}
