using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }

    [SerializeField]
    private List<Player> players = new List<Player>();

    [SerializeField]
    private int totalRounds = 3;

    private int currentRound = 0;

    private Button rollDiceButton;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rollDiceButton = GameObject.Find("RollDiceButton").GetComponent<Button>();
        StartGame();
    }

    private void StartGame()
    {
        ShufflePlayerOrder();
        StartCoroutine(PlayGame());
    }

    private void ShufflePlayerOrder()
    {
        var shuffled = players.OrderBy(_ => Guid.NewGuid()).ToList();
        players = shuffled;
        foreach (Player p in players)
        {
            Debug.Log(p);
            p.SetRollDiceButton(rollDiceButton);
        }

    }

    private IEnumerator PlayGame()
    {
        while (currentRound < totalRounds)
        {
            Debug.Log($"Round {currentRound + 1} 시작");
            yield return StartCoroutine(PlayRound());
            currentRound++;
        }

        EndGame();
    }

    private IEnumerator PlayRound()
    {
        foreach (var player in players)
        {
            Debug.Log($"{player.name}의 턴 시작");
            yield return StartCoroutine(player.TakeTurn());
        }
        yield return null;
    }

    private void EndGame()
    {
        Debug.Log("게임 종료!");
        // 승자 계산 또는 종료 로직 구현 가능
    }
}
