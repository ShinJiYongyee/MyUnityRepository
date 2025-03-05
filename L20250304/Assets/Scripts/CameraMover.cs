using Unity.Mathematics;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    Transform Player;
    float positionDelay = 2.0f;
    float rotationDelay = 2.0f;
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Player.position, positionDelay*Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Player.rotation, rotationDelay*Time.deltaTime);
    }
}
