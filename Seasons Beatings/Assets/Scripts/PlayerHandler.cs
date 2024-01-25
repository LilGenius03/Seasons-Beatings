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
    HealthSystem healthSystem;

    public SpriteRenderer body, head;

    private bool ready = false;
    private bool canReady = false;
    
    [SerializeField] TextMeshPro readyText;
    public SpriteRenderer readySpriteRenderer;
    [SerializeField] Sprite readySprite, unReadySprite;

    public GameObject[] hammerParts;

    bool freezedInputs = false;

    CharacterTables currentCharacter;

    public GameObject damageLight, damageHeavy;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        healthSystem = GetComponent<HealthSystem>();
        mover = GetComponent<Hammer>();
        StartCoroutine(SpawnDelay());
    }

    private void OnEnable()
    {
        GameManager.instance.FreezeInputs.AddListener(FreezeInputs);
        GameManager.instance.UnFreezeInputs.AddListener(UnFreezeInputs);
        GameManager.instance.OnGameOver.AddListener(PlayerGameOver);
        GameManager.instance.OnPreGameStarted.AddListener(DisableReady);
    }

    private void Update()
    {
        if (mover.Pivot.transform.eulerAngles.z > 0 && mover.Pivot.transform.eulerAngles.z < 180)
        {
            body.flipX = false;
            head.flipX = false;
        }
        
        if(mover.Pivot.transform.eulerAngles.z > 180)
        {
            body.flipX = true;
            head.flipX = true;
        }
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
            currentCharacter  = PlayerManager.instance.CheckAvailableCharacters(this, true);
            body.sprite = currentCharacter.spritesNormal[1];
            head.sprite = currentCharacter.spritesNormal[0];
            damageLight = Instantiate(currentCharacter.spritesSlightlyDamagedTurso, body.transform);
            damageHeavy = Instantiate(currentCharacter.spritesHeavyDamagedTurso, body.transform);
        }
        else if(ctx.performed && ctx.ReadValue<float>() < 0)
        {
            currentCharacter = PlayerManager.instance.CheckAvailableCharacters(this, false);
            body.sprite = currentCharacter.spritesNormal[1];
            head.sprite = currentCharacter.spritesNormal[0];
            damageLight = Instantiate(currentCharacter.spritesSlightlyDamagedTurso, body.transform);
            damageHeavy = Instantiate(currentCharacter.spritesHeavyDamagedTurso, body.transform);
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
                readySpriteRenderer.sprite = readySprite;
            else
                readySpriteRenderer.sprite = unReadySprite;
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

    public void DisableReady()
    {
        readySpriteRenderer.gameObject.SetActive(false);
        canReady = false;
    }

    public void ResetPlayer()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        mover.SetInputVector(Vector2.zero);
        mover.SetRetractValue(0);
        mover.Pivot.transform.eulerAngles = new Vector3(0, 0, 0);
        healthSystem.HealthReset();
    }

    public void PlayerGameOver()
    {
        mover.hammerSpeed *= 0.2f;
    }

}
