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
        if (characterList[players.Count - 1] == 0)
            handler.graphics.sprite = characterSprites[players.Count - 1];
        else
        {

        }

    }

/*    public Sprite CheckAvailableCharacters(int playerNum, bool inc = true)
    {
        return 10;
    }*/

    void InitialiseCharacterList()
    {
        characterList.Add(0, 0);
        characterList.Add(1, 0);
        characterList.Add(2, 0);
        characterList.Add(3, 0);
    }
}
