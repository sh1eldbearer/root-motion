using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeGame : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    // Player objects preexist in the game scene so they don't need to be instantiated
    [Header("Player GameObjects")]
    [SerializeField] private GameObject _p1Object;
    [SerializeField] private GameObject _p2Object;
    [SerializeField] private GameObject _p3Object;
    [SerializeField] private GameObject _p4Object;

    [Space, SerializeField] private SingleCameraController _singlePlayerCamera; 

#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// The Player 1 GameObject.
    /// </summary>
    public GameObject P1Object
    {
        get { return _p1Object; }
    }

    /// <summary>
    /// The Player 2 GameObject.
    /// </summary>
    public GameObject P2Object
    {
        get { return _p2Object; }
    }

    /// <summary>
    /// The Player 3 GameObject.
    /// </summary>
    public GameObject P3Object
    {
        get { return _p3Object; }
    }

    /// <summary>
    /// The Player 4 GameObject.
    /// </summary>
    public GameObject P4Object
    {
        get { return _p4Object; }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        foreach (PlayerData player in GameManager.gm.PlayerInfo)
        {
            if (player.Status == PlayerStatus.Ready)
            {
                CreatePlayer(player);
            }
        }

        GameManager.gm.UnpauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreatePlayer(PlayerData player)
    {
        GameObject playerObject = null;
        Transform spawnPoint = null;

        // Gets the appropriate player objects depending on which player is being created
        switch (player.PlayerNumber)
        {
            case PlayerNumbers.P1:
                playerObject = _p1Object;
                spawnPoint = GameManager.gm.CurrentRoomData.P1SpawnPoint;
                break;
            case PlayerNumbers.P2:
                playerObject = _p2Object;
                spawnPoint = GameManager.gm.CurrentRoomData.P2SpawnPoint;
                break;
            case PlayerNumbers.P3:
                playerObject = _p3Object;
                spawnPoint = GameManager.gm.CurrentRoomData.P3SpawnPoint;
                break;
            case PlayerNumbers.P4:
                playerObject = _p4Object;
                spawnPoint = GameManager.gm.CurrentRoomData.P4SpawnPoint;
                break;
        }

        // Attaches a character model to the player object
        GameObject newPlayer = Instantiate(SkinManager.skinMgr.SkinColors[player.SkinColorIndex].Model,
            playerObject.transform);
        // Places the player at the appropriate spawn point
        playerObject.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

        // Activates the player object and its camera
        playerObject.SetActive(true);

        if (player.PlayerNumber == PlayerNumbers.P1)
        {
            _singlePlayerCamera.SetFollowTarget(newPlayer);
        }
        else
        {
            // TODO: Change camera assignment behavior for multiplayer
            newPlayer.GetComponent<AgentData>().AssignCameraController(_singlePlayerCamera);
        }
    }
}
