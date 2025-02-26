using UnityEngine;

public class PlayerAim : MonoBehaviour
{

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        Vector3 direction = new Vector3(mousePosition.x-transform.position.x, mousePosition.y-transform.position.y, 0);

        transform.up = direction;
    }
}
