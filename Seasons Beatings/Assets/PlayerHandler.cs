using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerHandler : MonoBehaviour
{
    public int playerNum;
    private FakePlayerMovement mover;
    private PlayerInput playerInput;

    public SpriteRenderer graphics;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        mover = GetComponent<FakePlayerMovement>();
    }

    public void OnHammerMovement(CallbackContext ctx)
    {
        mover.SetInputVector(ctx.ReadValue<Vector2>());
    }

    public void OnRetractHammer(CallbackContext ctx)
    {
        if (ctx.performed)
            Debug.Log("Ahhh");
    }

    public void ChangeOutfit(CallbackContext ctx)
    {
        if(ctx.performed && ctx.ReadValue<float>() < 0)
        {
            //graphics.sprite = PlayerManager.instance.CheckAvailableCharacters(playerNum, true);
        }
        else if(ctx.performed && ctx.ReadValue<float>() > 0)
        {
            //graphics.sprite = PlayerManager.instance.CheckAvailableCharacters(playerNum, false);
        }
    }
}
