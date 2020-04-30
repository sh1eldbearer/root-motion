using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealthUI : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The PawnData component for this pawn."),
        SerializeField] private PawnData _pawnData;
#pragma warning restore CS0649
    #endregion

    #region Public Properties

    #endregion
	
	// Awake is called before Start
	private void Awake()
	{
        // Component reference assignments
        if (_pawnData == null)
        {
            _pawnData = this.gameObject.GetComponentInParent<PawnData>();
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnEnable()
    {
        // Register listeners with the health manager
        _pawnData.HealthMgr.AddCurrentHealthChangedListener(StartUpdateCurrentHealthCoroutine);
        _pawnData.HealthMgr.AddMaxHealthChangedListener(UpdateMaxHealth);
    }

    private void OnDisable()
    {
        // Unregister listeners with the health manager
        _pawnData.HealthMgr.RemoveCurrentHealthChangedListener(StartUpdateCurrentHealthCoroutine);
        _pawnData.HealthMgr.RemoveMaxHealthChangedListener(UpdateMaxHealth);
    }

    private void StartUpdateCurrentHealthCoroutine()
    {
        StartCoroutine(UpdateCurrentHealth());
    }

    /// <summary>
    /// Updates the displayed value of the health slider over a designated period of time.
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateCurrentHealth()
    {
        while (_pawnData.HealthSlider.value != _pawnData.CurrentHealth)
        {
            _pawnData.HealthSlider.value = Mathf.Lerp(_pawnData.HealthSlider.value, _pawnData.CurrentHealth,
                Time.unscaledDeltaTime * GameManager.gm.HealthSliderUpdateTime);
            yield return null;
        }
    }

    /// <summary>
    /// Immediately updates the max value of the health slider.
    /// </summary>
    private void UpdateMaxHealth()
    {
        _pawnData.HealthSlider.maxValue = _pawnData.MaxHealth;
    }
}
