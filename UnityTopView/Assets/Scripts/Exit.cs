using UnityEngine;

public enum ExitDirection
{
    right, left, down, up
}
public class Exit : MonoBehaviour
{
    public string sceneName = "";
    public int doorNumber = 0;
    public ExitDirection exitDirection = ExitDirection.down;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //전환될 씬을 관리할 RoomManager
            RoomManager.ChangeScene(sceneName, doorNumber); 
        }
    }
}
