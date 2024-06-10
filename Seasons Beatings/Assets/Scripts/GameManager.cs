using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameModes
{
    Classic,
    RaceToTheTop
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    PlayerInputManager playerInputManager;
    private void Awake()
    {
        if(instance != null)
        {
            //Debug.Log("Error more than one " + name + " component found");
            Destroy(gameObject);
            return;
        }
        instance = this;

        playerInputManager = GetComponent<PlayerInputManager>();
        DontDestroyOnLoad(gameObject);
    }

    public UnityEvent OnResetGame, OnPreGameStarted, OnGameStarted, OnRoundReset, OnGameOver;
    public UnityEvent FreezeInputs, UnFreezeInputs;

    public bool gameStarted, gameOver;

    public int score2Win;
    [SerializeField] ScoreUIHandler scoreUIHandler;

    //UI
    [SerializeField] GameObject startUI;
    [SerializeField] float countdownTime = 3;
    [SerializeField] TextMeshProUGUI countdownText;

    //GameModes
    public GameModes currentGameMode;
    int id_currentGameMode;

    //Camera
    [SerializeField] Camera cam;
    bool lerpCam = false;
    [SerializeField] float normCamSize, endCamSize, endCamZoomSpeed, endCamMoveSpeed;
    Vector3 ogcampos;

    Transform winner, loser;

    int playersReady = 0;
    public int playersNeeded = 2;

    List<PlayerInput> playersAlive = new List<PlayerInput>();

    private void Start()
    {
        StartCoroutine(JoinDelay());
        ogcampos = cam.transform.position;
    }

    private void Update()
    {
        if (lerpCam)
            CamLerp();
    }

    public void ResetGame()
    {
        PlayerManager.instance.ResetPlayers();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        playersAlive.Clear();
        winner = null;
        loser = null;

        playersReady = 0;

        cam.transform.position = ogcampos;
        cam.orthographicSize = normCamSize;
        lerpCam = false;

        gameStarted = false;
        gameOver = false;

        OnResetGame.Invoke();
        UnFreezeInputs.Invoke();
    }

    IEnumerator StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ModeManager.instance.PreStartGame();
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
        ModeManager.instance.StartGame();
        OnGameStarted.Invoke();
        gameStarted = true;
        playersAlive.AddRange(PlayerManager.instance.players);
        PlayerManager.instance.testinyo = true;
    }

    public IEnumerator ResetRound()
    {
        FreezeInputs.Invoke();
        //PlayerManager.instance.ResetPlayers();
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

    IEnumerator GameOver(Transform theWinner)
    {
        gameOver = true;
        winner = theWinner;
        lerpCam = true;
        Debug.Log("Player " + winner.name + " Won!");
        Time.timeScale = 0.05f;
        OnGameOver.Invoke();
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(5f);
        ResetGame();
        SceneManager.LoadScene(id_currentGameMode + 1);
    }

    public void PlayGameOver(Transform theWinner)
    {
        StartCoroutine(GameOver(theWinner));
    }

    IEnumerator JoinDelay()
    {
        yield return new WaitForSecondsRealtime(3f);
        playerInputManager.EnableJoining();
    }

    public void PlayerDied(PlayerHandler playerDead)
    {               
        playersAlive.Remove(playerDead.GetComponent<PlayerInput>());

        if(playersAlive.Count == 1)
        {
            PlayGameOver(playersAlive[0].transform);
        }
    }

    public void PlayerReady(bool ready)
    {
        if (ready)
            playersReady++;
        else
            playersReady--;
        if (playersReady > 1 && playersReady == PlayerManager.instance.players.Count)
            StartCoroutine(StartGame());
    }

    public void ChangeGameMode(int increment)
    {
        id_currentGameMode += increment;
        if (id_currentGameMode > 1)
            id_currentGameMode = 0;
        if (id_currentGameMode < 0)
            id_currentGameMode = 1;

        switch (id_currentGameMode)
        {
            case 0:
                currentGameMode = GameModes.Classic;
                SceneManager.LoadScene(1);
                break;
            case 1:
                currentGameMode = GameModes.RaceToTheTop;
                SceneManager.LoadScene(2);
                break;
        }
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
