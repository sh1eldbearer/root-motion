using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Enums;

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

#pragma warning restore CS0649
    #endregion
    
    // Start is called before the first frame update
    private void Awake()
    {
        // Checks to see if there is at least 1 ready player (if not, the game was started in the game scene)
        List<PlayerTracking> players = (from player in GameManager.gm.PlayerInfo where (int)player.Status >= 0 select player).ToList();
        
        if (players.Count == 0) // Testing mode (started from the game scene)
        {
            // Places the player at the player 1 spawn point
            _testPlayerObject.transform.SetPositionAndRotation(GameManager.gm.CurrentRoomData.P1SpawnPoint.transform.position,
                GameManager.gm.CurrentRoomData.P1SpawnPoint.transform.rotation);

            // Assigns the test player's PawnData to the game manager and camera
            GameManager.gm.PlayerInfo[0].SetPawnData(_testPlayerObject.GetComponent<PawnData>());
            GameManager.gm.PlayerInfo[0].PawnData.HealthMgr.InitializeHealthValues(GameManager.gm.InitialPlayerHealth);
            GameManager.gm.GameCameraController.SetFollowTarget(_testPlayerObject);

            // Sets the player indicator of the test player to a random color
            GameManager.gm.PlayerInfo[0].PawnData.SetPlayerIndicatorColor(SkinManager.skinMgr.GetRGBColor(Random.Range(0, 8)));
        }
        else // Standard game mode (started from the menu scene)
        {
            // We don't want the test player to exist, so get rid of it
            Destroy(_testPlayerObject);

            // Instantiate all active players into the game scene
            foreach (PlayerTracking player in players)
            {
                if (player.Status == PlayerStatus.Ready)
                {
                    CreatePlayer(player);

                    // If there is only 1 active player, assigns the camera to follow that player
                    if (players.Count == 1)
                    {
                        GameManager.gm.GameCameraController.SetFollowTarget(player.PawnData.gameObject);
                    }
                }
            }
        }

        PauseManager.pauseMgr.UnpauseGame();

        // Destroys this object after a few seconds
        Destroy(this.gameObject, 2f);
    }

    /// <summary>
    /// Assigns the chosen character model to the appropriate player, and spawns them into the game world
    /// </summary>
    /// <param name="player">The information about the player being spawned.</param>
    private void CreatePlayer(PlayerTracking player)
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

        // Activates the player object and its camera
        playerObject.SetActive(true);
        // Assigns the new character's agent data component to the player info array
        player.SetPawnData(playerObject.GetComponent<PawnData>());
        // Initializes the player's health
        GameManager.gm.PlayerInfo[0].PawnData.HealthMgr.InitializeHealthValues(GameManager.gm.InitialPlayerHealth);
        // Assigns the selected skin color materials to the player object
        SkinManager.skinMgr.AssignMaterials(player.SkinColorIndex, player.PawnData);
        // Assigns the chosen player color to the player indicator
        player.PawnData.SetPlayerIndicatorColor(SkinManager.skinMgr.GetRGBColor(player.SkinColorIndex));
        // Places the player at the appropriate spawn point
        playerObject.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
    }
}
