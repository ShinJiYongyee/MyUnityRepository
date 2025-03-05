using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 5.0f;

    void Start()
    {
        Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        //transform.Translate( Vector3.up * speed * Time.deltaTime, Space.Self );
    }
}
