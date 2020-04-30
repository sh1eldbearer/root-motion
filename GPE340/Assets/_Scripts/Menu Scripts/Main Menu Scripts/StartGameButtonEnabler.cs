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
    }
}
