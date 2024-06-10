using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClassicMode : ModeManager
{
    GameManager gm;
    PlayerManager pm;

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
        pm.MakePlayersMune();
    }

    public override void StartGame()
    {
        base.StartGame();
    }

    public override void EndGame(Transform theWinner)
    {
        gm.PlayGameOver(theWinner);
    }


}
