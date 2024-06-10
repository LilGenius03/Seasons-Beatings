using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaceToTheTopMode : ModeManager
{
    GameManager gm;
    PlayerManager pm;

    List<PlayerInput> playersFinished = new List<PlayerInput>();
    bool gameEnded;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        pm = PlayerManager.instance;

        //layouts[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void PreStartGame()
    {
        base.PreStartGame();
        pm.MakePlayersImmune();
    }

    public override void StartGame()
    {
        base.StartGame();
    }

    public override void EndGame(Transform theWinner)
    {
        gm.PlayGameOver(theWinner);
    }

    public void PlayerFinished(PlayerInput playerFin)
    {
        if (playersFinished.Contains(playerFin) || gameEnded)
            return;

        playersFinished.Add(playerFin);
        PlayerHandler pHandler = playerFin.GetComponent<PlayerHandler>();
        pHandler.FreezeInputs();

        if (playersFinished.Count == pm.players.Count)
        {
            gameEnded = true;
            EndGame(playersFinished[0].transform);
        }
    }
}
