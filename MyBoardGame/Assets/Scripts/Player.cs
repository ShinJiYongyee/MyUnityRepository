using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private List<GameObject> tiles = new List<GameObject>();
    [SerializeField] private int minDiceValue = 1;
    [SerializeField] private int maxDiceValue = 6;
    [SerializeField] private Button rollDiceButton;
    [SerializeField] private GameObject d6Dice;
    [SerializeField] private float diceLifeTime = 1.0f;
    private int currentTileIndex = 0;
    private int diceValue = 0;
    private bool isTurnComplete = false;
    private bool isWaitingForInput = false;


    public void SetRollDiceButton(Button button)
    {
        rollDiceButton = button;
    }


    public IEnumerator TakeTurn()
    {
        isTurnComplete = false;
        isWaitingForInput = true;
        rollDiceButton.interactable = true;
        rollDiceButton.onClick.AddListener(OnClickRollDice);

        // 입력 대기
        while (!isTurnComplete)
        {
            yield return null;
        }

        // 정리
        rollDiceButton.interactable = false;
        rollDiceButton.onClick.RemoveAllListeners();
    }

    public void OnClickRollDice()
    {
        if (!isWaitingForInput) return;
       
        diceValue = Random.Range(minDiceValue, maxDiceValue + 1);
        Debug.Log($"{gameObject.name} 주사위 굴림: {diceValue}");
        isWaitingForInput = false;
        StartCoroutine(RollDiceAndMove());
    }

    private IEnumerator RollDiceAndMove()
    {
        yield return StartCoroutine(InstantiateDice());
        yield return StartCoroutine(MovePlayer(diceValue));
    }

    private IEnumerator MovePlayer(int diceValue)
    {
        for (int i = 0; i < diceValue; i++)
        {
            currentTileIndex = (currentTileIndex + 1) % tiles.Count;
            Transform targetPosition = tiles[currentTileIndex].transform;
            yield return StartCoroutine(MoveToTargetPosition(targetPosition));

        }

        isTurnComplete = true;
    }

    private IEnumerator MoveToTargetPosition(Transform target)
    {
        float speed = 5f;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(target.position.x, target.position.y + 0.7f, target.position.z);
        float distance = Vector3.Distance(startPos, endPos);
        float duration = distance / speed;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos; // 최종 위치 보정
    }

    private IEnumerator InstantiateDice()
    {
        GameObject diceInstance = Instantiate(d6Dice, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(diceLifeTime);
        Destroy(diceInstance);
    }
}
