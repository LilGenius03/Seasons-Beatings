using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerManager : MonoBehaviour
{
    public List<PlayerInput> players = new List<PlayerInput>();

    private PlayerInputManager playerInputManager;

    [SerializeField] private List<LayerMask> playerLayers;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    [SerializeField] Sprite[] characterSprites;
    private Dictionary<int, int> characterList = new Dictionary<int, int>(); //Keeps track of which character each player is playing as - <character, player>

    [SerializeField] private GameObject spawnEffect;

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
        player.name = "Player" + players.Count;
        player.GetComponent<PlayerHandler>().playerNum = players.Count;
        SetLayers(player);
        SetVisuals(player);
        player.transform.position = spawnPoints[players.Count - 1].position;
    }

    void SetLayers(PlayerInput player)
    {
        int _playerLayerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);
        player.gameObject.layer = _playerLayerToAdd;
    }

    void SetVisuals(PlayerInput player)
    {
        PlayerHandler handler = player.GetComponent<PlayerHandler>();
        if (characterList[handler.playerNum - 1] == 0)
        {
            handler.graphics.sprite = characterSprites[handler.playerNum - 1];
            characterList[handler.playerNum - 1] = handler.playerNum;
            handler.characterNum = handler.playerNum - 1;
        }
        else
        {
            for(int i = 0; i < characterSprites.Length; i++)
            {
                if(characterList[i] == 0)
                {
                    handler.graphics.sprite = characterSprites[i];
                    characterList[i] = handler.playerNum;
                    handler.characterNum = i;
                    break;
                }

            }
        }
        GameObject effect = Instantiate(spawnEffect, handler.graphics.transform);
        Destroy(effect, 1f);
    }

    public Sprite CheckAvailableCharacters(PlayerHandler handler, bool inc = true)
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
        GameObject effect = Instantiate(spawnEffect, handler.graphics.transform);
        Destroy(effect, 1f);
        return characterSprites[newNum];
    }

    void InitialiseCharacterList()
    {
        characterList.Add(0, 0);
        characterList.Add(1, 0);
        characterList.Add(2, 0);
        characterList.Add(3, 0);
    }
}
