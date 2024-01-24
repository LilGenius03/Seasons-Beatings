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
    private Hammer mover;
    private PlayerInput playerInput;

    public SpriteRenderer graphics;

    private bool ready = false;
    private bool canReady = false;
    
    [SerializeField] TextMeshPro readyText;

    public GameObject[] hammerParts;

    bool freezedInputs = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        mover = GetComponent<Hammer>();
        StartCoroutine(SpawnDelay());
    }

    private void OnEnable()
    {
        GameManager.instance.FreezeInputs.AddListener(FreezeInputs);
        GameManager.instance.UnFreezeInputs.AddListener(UnFreezeInputs);
    }

    #region Control Inputs
    public void OnHammerMovement(CallbackContext ctx)
    {
        if (!GameManager.instance.gameStarted || freezedInputs)
            return;
        mover.SetInputVector(ctx.ReadValue<Vector2>());
    }

    public void OnRetractHammer(CallbackContext ctx)
    {
        if (GameManager.instance.gameStarted && !freezedInputs)
        {
            if (ctx.performed)
                mover.SetRetractValue(ctx.ReadValue<float>());
            else
                mover.SetRetractValue(0f);
        }
    }

    public void ChangeOutfit(CallbackContext ctx)
    {
        if (ready || GameManager.instance.gameStarted || freezedInputs)
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
        if (ready || GameManager.instance.gameStarted || freezedInputs)
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

    public void ReadyUp(CallbackContext ctx)
    {
        if (ctx.performed && canReady && !GameManager.instance.gameStarted || freezedInputs)
        {
            ready = !ready;
            GameManager.instance.PlayerReady(ready);
            if (ready)
                readyText.text = "";
            else
                readyText.text = "NOT READY";
        }
    }
    #endregion

    public void FreezeInputs()
    {
        freezedInputs = true;
    }

    public void UnFreezeInputs()
    {
        freezedInputs = false;
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        canReady = true;
    }

    public void ResetPlayer()
    {
        mover.SetInputVector(Vector2.zero);
        mover.Pivot.transform.eulerAngles = new Vector3(0, 0, 0);
    }

}
