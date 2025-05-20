using UnityEditor.Purchasing;
using UnityEngine;

public class RollScript : MonoBehaviour
{

    public float RollSpeed = 270.0f;

    void Update()
    {
        transform.Rotate(Time.deltaTime * RollSpeed % 360, Time.deltaTime * RollSpeed % 360, Time.deltaTime * RollSpeed % 360);
    }
}
