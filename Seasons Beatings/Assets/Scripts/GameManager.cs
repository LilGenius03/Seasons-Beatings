using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    PlayerInputManager playerInputManager;
    public GameObject SettingsButton;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Error more than one " + name + " component found");
            return;
        }
        instance = this;

        playerInputManager = GetComponent<PlayerInputManager>();
    }

    public UnityEvent OnPreGameStarted, OnGameStarted, OnRoundReset, OnGameOver;
    public UnityEvent FreezeInputs, UnFreezeInputs;

    public bool gameStarted, gameOver;

    int playersReady = 0;
    [SerializeField] int playersNeeded = 2;

    int[] playerScores = new int[4];
    [SerializeField] public int score2Win;
    [SerializeField] ScoreUIHandler scoreUIHandler;

    //UI
    [SerializeField] GameObject startUI;
    [SerializeField] float countdownTime = 3;
    [SerializeField] TextMeshProUGUI countdownText;

    [SerializeField] GameObject[] layouts;
    int currentLayout;

    [SerializeField] Camera cam;
    bool lerpCam = false;
    [SerializeField] float normCamSize, endCamSize, endCamZoomSpeed, endCamMoveSpeed;
    Transform winner, loser;

    private void Start()
    {
        StartCoroutine(JoinDelay());
    }

    private void Update()
    {
        if (lerpCam)
            CamLerp();
    }

    IEnumerator StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SettingsButton.SetActive(false);
        playerInputManager.DisableJoining();
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

    public IEnumerator ResetRound()
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

    IEnumerator GameOver(int playerNum, int otherPlayerNum)
    {
        gameOver = true;
        winner = PlayerManager.instance.players[playerNum - 1].transform;
        loser = PlayerManager.instance.players[otherPlayerNum - 1].transform;
        lerpCam = true;
        Debug.Log("Player " + playerNum + " Won!");
        Time.timeScale = 0.05f;
        OnGameOver.Invoke();
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(5f);
        SceneManager.LoadScene(0);
    }

    IEnumerator JoinDelay()
    {
        yield return new WaitForSecondsRealtime(2f);
        playerInputManager.EnableJoining();
    }

    public void IncreaseScore(int playerNum, int otherPlayerNum)
    {
        playerScores[playerNum-1]++;
        scoreUIHandler.UpdateScores(playerScores);
        if (playerScores[playerNum - 1] == score2Win)
            StartCoroutine(GameOver(playerNum, otherPlayerNum));
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

    void CamLerp()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, endCamSize, endCamZoomSpeed * Time.unscaledDeltaTime);
        //float camPosX = (winner.position.x - loser.position.x) / 2;
        float lerpedX = Mathf.Lerp(cam.transform.position.x, winner.transform.position.x, endCamMoveSpeed * Time.unscaledDeltaTime);
        float lerpedY = Mathf.Lerp(cam.transform.position.y, winner.transform.position.y + 2.5f, endCamMoveSpeed * Time.unscaledDeltaTime);
        cam.transform.position = new Vector3(lerpedX, lerpedY, cam.transform.position.z);
    }
}
