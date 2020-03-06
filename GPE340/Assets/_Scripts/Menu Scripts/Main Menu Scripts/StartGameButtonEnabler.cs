using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButtonEnabler : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The Button component for this button."),
        SerializeField] private Button _button;
#pragma warning restore CS0649
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        // Component reference assignments
        if (_button == null)
        {
            _button = this.gameObject.GetComponent<Button>();
        }

        StartCoroutine(MonitorReadyStatus());
    }

    /// <summary>
    /// Waits until all joined players have selected a color before activating the Start Game button.
    /// </summary>
    /// <returns>Null.</returns>
    private IEnumerator MonitorReadyStatus()
    {
        while (true)
        {
            // All players that have joined the game must have selected a color in order for the game to start
            if (((int)GameManager.gm.PlayerInfo[0].Status < 0 || GameManager.gm.PlayerInfo[0].Status == PlayerStatus.Ready) &&
                ((int)GameManager.gm.PlayerInfo[1].Status < 0 || GameManager.gm.PlayerInfo[1].Status == PlayerStatus.Ready) &&
                ((int)GameManager.gm.PlayerInfo[2].Status < 0 || GameManager.gm.PlayerInfo[2].Status == PlayerStatus.Ready) &&
                ((int)GameManager.gm.PlayerInfo[3].Status < 0 || GameManager.gm.PlayerInfo[3].Status == PlayerStatus.Ready))
            {
                _button.interactable = true;
            }
            else
            {
                _button.interactable = false;
            }

            yield return null;
        }
    }
}
