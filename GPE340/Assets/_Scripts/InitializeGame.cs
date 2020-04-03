using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InitializeGame : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    // Player objects preexist in the game scene so they don't need to be instantiated
    [Header("Player GameObjects")]
    [SerializeField] private GameObject _testPlayerObject;
    [SerializeField] private GameObject _p1Object;
    [SerializeField] private GameObject _p2Object;
    [SerializeField] private GameObject _p3Object;
    [SerializeField] private GameObject _p4Object;

    [Space, SerializeField] private CameraController _gameCamera; 

#pragma warning restore CS0649
    #endregion
    
    // Start is called before the first frame update
    private void Awake()
    {
        List<PlayerData> players = (from player in GameManager.gm.PlayerInfo where (int)player.Status >= 0 select player).ToList();
        if (players.Count == 0)
        {
            // Places the player at the player 1 spawn point
            _testPlayerObject.transform.SetPositionAndRotation(_p1Object.transform.position, _p1Object.transform.rotation);
            GameManager.gm.PlayerInfo[0].SetAgentData(_testPlayerObject.GetComponent<PawnData>());
            _gameCamera.SetFollowTarget(_testPlayerObject);
        }
        else
        {
            Destroy(_testPlayerObject);
        }

        foreach (PlayerData player in GameManager.gm.PlayerInfo)
        {
            if (player.Status == PlayerStatus.Ready)
            {
                CreatePlayer(player);
            }
        }

        GameManager.gm.UnpauseGame();
    }

    /// <summary>
    /// Assigns the chosen character model to the appropriate player, and spawns them into the game world
    /// </summary>
    /// <param name="player">The information about the player being spawned.</param>
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
        // Assigns the new character's agent data component to the player info array
        player.SetAgentData(newPlayer.GetComponent<PawnData>());

        if (player.PlayerNumber == PlayerNumbers.P1)
        {
            _gameCamera.SetFollowTarget(newPlayer);
        }
    }
}
