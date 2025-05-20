using UnityEngine;

// 기능 : 마우스 버튼을 Unity 애플리케이션에 전달한다.
public class GridPosition : MonoBehaviour
{
    [SerializeField] private int _x;
    [SerializeField] private int _y;

    private void OnMouseDown()
    {
        Logger.Info($"({_x}, {_y}) coord clicked.");

        GameManager.Instance.PlayMarker(_x, _y);
    }
}