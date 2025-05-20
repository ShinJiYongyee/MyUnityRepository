using System;
using UnityEngine;

public class HudUI : MonoBehaviour
{
    [SerializeField] private GameObject _circleIndicatorArrow;
    [SerializeField] private GameObject _crossIndicatorArrow;

    void Start()
    {
        _circleIndicatorArrow.SetActive(false);
        _crossIndicatorArrow.SetActive(false);

        GameManager.Instance.OnTurnChanged += ChangeIndicator;
    }

    private void ChangeIndicator(SquareState currentTurn)
    {
        switch (currentTurn)
        {
            case SquareState.None:
                _circleIndicatorArrow.SetActive(false);
                _crossIndicatorArrow.SetActive(false);
                break;
            case SquareState.Cross:
                _circleIndicatorArrow.SetActive(false);
                _crossIndicatorArrow.SetActive(true);
                break;
            case SquareState.Circle:
                _circleIndicatorArrow.SetActive(true);
                _crossIndicatorArrow.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException($"{(int)currentTurn}");
                break;
        }

    }
}