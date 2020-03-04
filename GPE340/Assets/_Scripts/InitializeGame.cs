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

        // Gets the appropriate player object depending on which player is being created
        switch (player.PlayerNumber)
        {
            case PlayerNumbers.P1:
                playerObject = _p1Object;
                break;
            case PlayerNumbers.P2:
                playerObject = _p2Object;
                break;
            case PlayerNumbers.P3:
                playerObject = _p3Object;
                break;
            case PlayerNumbers.P4:
                playerObject = _p4Object;
                break;
        }

        // Attaches a character model to the player object
        GameObject newPlayer = Instantiate(SkinManager.skinMgr.SkinColors[player.SkinColorIndex].Model,
            playerObject.transform);
        // Places the player at the appropriate spawn point
        playerObject.transform.SetPositionAndRotation(GameManager.gm.CurrentRoomData.P1SpawnPoint.position,
            GameManager.gm.CurrentRoomData.P1SpawnPoint.rotation);
        // Assigns the models' transform the player's agent data
        playerObject.GetComponent<AgentData>().SetAgentTransform(newPlayer.transform);
        // Assigns the model's avatar to the player's animator
        playerObject.GetComponent<Animator>().avatar =
            SkinManager.skinMgr.SkinColors[player.SkinColorIndex].ModelAvatar;

        // Activates the player object and its camera
        playerObject.SetActive(true);

        if (player.PlayerNumber == PlayerNumbers.P1)
        {
            _singlePlayerCamera.SetFollowTarget(playerObject);
        }
    }
}
