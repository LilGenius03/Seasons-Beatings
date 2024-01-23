using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerHandler : MonoBehaviour
{
    public int playerNum;
    public int characterNum;
    private FakePlayerMovement mover;
    private PlayerInput playerInput;

    public SpriteRenderer graphics;

    private bool ready = false;
    private bool canReady = false;
    
    [SerializeField] TextMeshPro readyText;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        mover = GetComponent<FakePlayerMovement>();
        StartCoroutine(SpawnDelay());
    }

    public void OnHammerMovement(CallbackContext ctx)
    {
        if (!GameManager.instance.gameStarted)
            return;
        mover.SetInputVector(ctx.ReadValue<Vector2>());
    }

    public void OnRetractHammer(CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (GameManager.instance.gameStarted)
                Debug.Log("Ahhh");
            else if(canReady)
            {
                ready = !ready;
                GameManager.instance.PlayerReady(ready);
                if (ready)
                    readyText.text = "";
                else
                    readyText.text = "NOT READY";
            }

        }
    }

    public void ChangeOutfit(CallbackContext ctx)
    {
        if (ready || GameManager.instance.gameStarted)
            return;
        if(ctx.performed && ctx.ReadValue<float>() > 0)
        {
            graphics.sprite = PlayerManager.instance.CheckAvailableCharacters(this, true);
        }
        else if(ctx.performed && ctx.ReadValue<float>() < 0)
        {
            graphics.sprite = PlayerManager.instance.CheckAvailableCharacters(this, false);
        }
    }

    public void ChangeLayout(CallbackContext ctx)
    {
        if (ready || GameManager.instance.gameStarted)
            return;
            if (ctx.performed && ctx.ReadValue<float>() > 0)
        {
            GameManager.instance.ChangeLayout(true);
        }
        else if (ctx.performed && ctx.ReadValue<float>() < 0)
        {
            GameManager.instance.ChangeLayout(false);
        }
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        canReady = true;
    }
}
