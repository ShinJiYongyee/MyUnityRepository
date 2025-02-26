using UnityEngine;

public class Background : MonoBehaviour
{
    public Material background;
    public float scrollSpeed = 0.05f;


    void Update()
    {
        Vector2 dir = Vector2.up;
        background.mainTextureOffset += dir.normalized * scrollSpeed * Time.deltaTime;
    }
}
