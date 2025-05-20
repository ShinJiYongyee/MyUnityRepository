using UnityEngine;

/// <summary>
/// 보드의 상태를 애플리케이션에 출력한다.
/// </summary>
public class GameVisualManager : MonoBehaviour
{
    [SerializeField] private GameObject _crossMarkerPrefab;
    [SerializeField] private GameObject _circleMarkerPrefab;

    private void Start()
    {
        // 이벤트를 구독한다.
        // 마커의 생성은 보드가 바뀌었을 때 이뤄져야 한다.
        GameManager.Instance.OnBoardChanged += CreateMarker;
    }

    private void CreateMarker(int x, int y, SquareState boardState)
    {
        switch (boardState)
        {
            case SquareState.Cross:
                Instantiate(_crossMarkerPrefab, GetWorldPositionFromCoordinate(x, y), Quaternion.identity);
                break;
            case SquareState.Circle:
                Instantiate(_circleMarkerPrefab, GetWorldPositionFromCoordinate(x, y), Quaternion.identity);
                break;
            default:
                Logger.Error($"잘못된 값이 입력되었습니다. {(int)boardState}");
                break;
        }
    }

    private Vector2 GetWorldPositionFromCoordinate(int x, int y)
    {
        // (0, 0) => Vector2(-3, 3)
        // (1, 0) => Vector2(0, 3)
        // (2, 0) => Vector2(3, 3)

        // (0, 1) => Vector2(-3, 0)
        // (1, 1) => Vector2(0, 0)
        // (2, 1) => Vector2(3, 0)

        // (0, 2) => Vector2(-3, -3)
        // (1, 2) => Vector2(0, -3)
        // (2, 2) => Vector2(3, -3)

        // x
        // 0 => -3, 1 => 0, 2 => 3
        int worldX = -3 + 3 * x;

        // y
        // 0 => 3, 1 => 0, 2 => -3
        int worldY = 3 - 3 * y;

        return new Vector2(worldX, worldY);
    }


}
