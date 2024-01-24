using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Error more than one " + name + " component found");
            return;
        }
        instance = this;
    }

    public UnityEvent OnPreGameStarted, OnGameStarted, OnRoundReset;
    public UnityEvent FreezeInputs, UnFreezeInputs;

    public bool gameStarted;

    int playersReady = 0;
    [SerializeField] int playersNeeded = 2;

    int[] playerScores = new int[4];
    [SerializeField] int score2Win;
    [SerializeField] ScoreUIHandler scoreUIHandler;

    //UI
    [SerializeField] GameObject startUI;
    [SerializeField] float countdownTime = 3;
    [SerializeField] TextMeshProUGUI countdownText;

    [SerializeField] GameObject[] layouts;
    int currentLayout;

    IEnumerator StartGame()
    {
        OnPreGameStarted.Invoke();
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }
        countdownText.text = "";
        countdownTime = 3f;
        OnGameStarted.Invoke();
        gameStarted = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            StartCoroutine(ResetRound());
    }

    IEnumerator ResetRound()
    {
        FreezeInputs.Invoke();
        PlayerManager.instance.ResetPlayers();
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }
        countdownText.text = "";
        countdownTime = 3f;
        UnFreezeInputs.Invoke();
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSecondsRealtime(1f);
    }

    public void IncreaseScore(int playerNum)
    {
        playerScores[playerNum-1]++;
        scoreUIHandler.UpdateScores(playerScores);
        if (playerScores[playerNum - 1] == score2Win)
            StartCoroutine(GameOver());
        else
            StartCoroutine(ResetRound());
    }

    public void PlayerReady(bool ready)
    {
        if (ready)
            playersReady++;
        else
            playersReady--;
        if (playersReady == playersNeeded)
            StartCoroutine(StartGame());
    }

    public void ChangeLayout(bool increase)
    {
        layouts[currentLayout].SetActive(false);
        if (increase)
        {
            currentLayout++;
            if (currentLayout >= layouts.Length)
                currentLayout = 0;
        }
        else
        {
            currentLayout--;
            if (currentLayout < 0)
                currentLayout = layouts.Length - 1;
        }
        layouts[currentLayout].SetActive(true);
    }
}
