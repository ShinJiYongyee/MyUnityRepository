using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float h = 0;
    void Start()
    {
       
    }

    void Update()
    {
        h += Input.GetAxis("Horizontal")*Time.deltaTime;
        float v = Input.GetAxis("Vertical");

        transform.rotation = quaternion.Euler(0,0,-h);
        transform.position += new Vector3(0, v, 0)*Time.deltaTime;

    }
}
