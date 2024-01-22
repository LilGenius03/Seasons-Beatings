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
