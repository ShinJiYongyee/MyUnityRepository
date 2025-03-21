using UnityEngine;

public class DoorLeafManager : MonoBehaviour
{
    public bool isOpened = false;

    public void ManipulateDoor()
    {
        isOpened = !isOpened;
        if (!isOpened)
        {
            transform.Rotate(0, 0, -90);
        }
        else
        {
            transform.Rotate(0, 0, 0);
        }
    }
}
