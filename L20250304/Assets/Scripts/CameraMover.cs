using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMover : MonoBehaviour
{
    Transform Player;
    float positionDelay = 2.0f;
    float rotationDelay = 2.0f;

    Vector3 currentVelocity;
    float smoothTime = 0.3f;
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = Vector3.Lerp(transform.position, Player.position, positionDelay*Time.deltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, Player.position, ref currentVelocity, smoothTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Player.rotation, rotationDelay*Time.deltaTime);
        
    }
}
