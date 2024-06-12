using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerManager : MonoBehaviour
{
    public List<PlayerInput> players = new List<PlayerInput>();

    private PlayerInputManager playerInputManager;

    [SerializeField] private List<LayerMask> playerLayers;
    [SerializeField] private List<LayerMask> hammerLayers;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    [SerializeField] CharacterTables[] characterSprites;
    private Dictionary<int, int> characterList = new Dictionary<int, int>(); //Keeps track of which character each player is playing as - <character, player>

    [SerializeField] private GameObject spawnEffect;
    [SerializeField] private ScoreUIHandler[] scoreUIHandlers;

    public bool testinyo;

    public static PlayerManager instance;
    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        InitialiseCharacterList();
        if (instance != null)
        {
            Debug.LogWarning("Error more than one " + name + " component found");
            return;
        }
        instance = this;
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    public void AddPlayer(PlayerInput player)
    {
        players.Add(player);
        player.transform.parent = transform;
        player.name = "Player" + players.Count;
        player.GetComponent<PlayerHandler>().playerNum = players.Count;
        player.GetComponent<PlayerHandler>().scoreUIHandler = scoreUIHandlers[players.Count - 1];
        //scoreUIHandlers[players.Count - 1].gameObject.SetActive(true);
        SetLayers(player);
        SetVisuals(player);
        player.transform.position = spawnPoints[players.Count - 1].position;

    }

    void SetLayers(PlayerInput player)
    {
        int _playerLayerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);
        int _hammerLayerToAdd = (int)Mathf.Log(hammerLayers[players.Count - 1].value, 2);
        player.gameObject.layer = _playerLayerToAdd;
        PlayerHandler handler = player.gameObject.GetComponent<PlayerHandler>();
        foreach(GameObject hammer in handler.hammerParts)
        {
            hammer.layer = _hammerLayerToAdd;
        }
    }

    public void SetLayerForPlayer(GameObject daPlayer, int playerNum)
    {
        int _playerLayerToAdd = (int)Mathf.Log(playerLayers[playerNum - 1].value, 2);
        daPlayer.layer = _playerLayerToAdd;
    }

    void SetVisuals(PlayerInput player)
    {
        PlayerHandler handler = player.GetComponent<PlayerHandler>();
        if (characterList[handler.playerNum - 1] == 0)
        {
            handler.body.sprite = characterSprites[handler.playerNum - 1].spritesNormal[1];
            handler.head.sprite = characterSprites[handler.playerNum - 1].spritesNormal[0];
            handler.currentCharacter = characterSprites[handler.playerNum - 1];
            handler.damageLight = Instantiate(characterSprites[handler.playerNum - 1].spritesSlightlyDamagedTurso, handler.body.transform);
            handler.damageHeavy = Instantiate(characterSprites[handler.playerNum - 1].spritesHeavyDamagedTurso, handler.body.transform);
            characterList[handler.playerNum - 1] = handler.playerNum;
            handler.characterNum = handler.playerNum - 1;
        }
        else
        {
            for(int i = 0; i < characterSprites.Length; i++)
            {
                if(characterList[i] == 0)
                {
                    handler.body.sprite = characterSprites[i].spritesNormal[1];
                    handler.head.sprite = characterSprites[i].spritesNormal[0];
                    handler.currentCharacter = characterSprites[i];
                    handler.damageLight = Instantiate(characterSprites[handler.playerNum - 1].spritesSlightlyDamagedTurso, handler.body.transform);
                    handler.damageHeavy = Instantiate(characterSprites[handler.playerNum - 1].spritesHeavyDamagedTurso, handler.body.transform);
                    characterList[i] = handler.playerNum;
                    handler.characterNum = i;
                    break;
                }

            }
        }
        if(players.Count % 2 != 0)
        {
            handler.body.transform.localScale = new Vector3(-1, 1, 1);
            //handler.readySpriteRenderer.flipX = true;
        }
        GameObject effect = Instantiate(spawnEffect, handler.body.transform);
        Destroy(effect, 1f);
    }

    public CharacterTables CheckAvailableCharacters(PlayerHandler handler, bool inc = true)
    {
        int newNum = handler.characterNum;
        if (inc)
        {
            while (characterList[newNum] != 0)
            {
                newNum++;
                if (newNum >= characterSprites.Length)
                    newNum = 0;
            }
        }
        else
        {
            while (characterList[newNum] != 0)
            {
                newNum--;
                if (newNum < 0)
                    newNum = characterSprites.Length - 1;
            }
        }
        characterList[handler.characterNum] = 0;
        characterList[newNum] = handler.playerNum;
        handler.characterNum = newNum;
        GameObject effect = Instantiate(spawnEffect, handler.body.transform);
        Destroy(effect, 1f);
        return characterSprites[newNum];
    }

    void InitialiseCharacterList()
    {
        characterList.Add(0, 0);
        characterList.Add(1, 0);
        characterList.Add(2, 0);
        characterList.Add(3, 0);
        characterList.Add(4, 0);
        characterList.Add(5, 0);
    }

    public void RespawnPlayer(int i, bool y = false)
    {
        PlayerHandler handy = players[i - 1].GetComponent<PlayerHandler>();
        handy.ResetPlayer(y);
        int ranSpawnpoint = Random.Range(0, spawnPoints.Count);
        players[i - 1].transform.position = spawnPoints[ranSpawnpoint].position;
        if (ranSpawnpoint % 2 != 0)
        {
            handy.body.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            handy.body.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void ResetPlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            PlayerHandler handy = players[i].GetComponent<PlayerHandler>();
            Rigidbody2D prb = handy.GetComponent<Rigidbody2D>();
            handy.ProperReset();
            players[i].transform.position = spawnPoints[i].position;
            if (i % 2 != 0)
            {
                handy.body.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                handy.body.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    public void MakePlayersImmune()
    {
        for (int i = 0; i < players.Count; i++)
        {
            HealthSystem player = players[i].GetComponent<HealthSystem>();
            player.allowDamage = false;
        }
    }

    public void MakePlayersMune()
    {
        for (int i = 0; i < players.Count; i++)
        {
            HealthSystem player = players[i].GetComponent<HealthSystem>();
            player.allowDamage = true;
        }
    }

    public void EnableScoreBoard()
    {
        for(int i = 0; i < players.Count; i++)
        {
            scoreUIHandlers[i].gameObject.SetActive(true);
            scoreUIHandlers[i].UpdateScores(3);
        }
    }
}
