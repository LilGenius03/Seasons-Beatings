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

    public UnityEvent OnPreGameStarted, OnGameStarted;

    public bool gameStarted;

    int playersReady = 0;
    int playersNeeded = 2;

    [SerializeField] GameObject startUI;
    [SerializeField] float countdownTime = 3;
    [SerializeField] TextMeshProUGUI countdownText;

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

    public void PlayerReady(bool ready)
    {
        if (ready)
            playersReady++;
        else
            playersReady--;
        if (playersReady == playersNeeded)
            StartCoroutine(StartGame());
    }
}
