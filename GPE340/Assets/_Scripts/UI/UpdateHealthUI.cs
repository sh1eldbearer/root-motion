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

        InitializeSliderValues();
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

    /// <summary>
    /// Initialize the values of the slider (usually at game start, on enable, or on respawn).
    /// </summary>
    public void InitializeSliderValues()
    {
        _pawnData.HealthSlider.value = _pawnData.CurrentHealth;
        _pawnData.HealthSlider.maxValue = _pawnData.MaxHealth;
    }

    /// <summary>
    /// Starts the update current health coroutine for this canvas.
    /// </summary>
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
                Time.smoothDeltaTime * GameManager.gm.HealthSliderUpdateTime);
            UpdateSliderColor();
            yield return null;
        }
    }

    /// <summary>
    /// Immediately updates the max value of the health slider.
    /// </summary>
    private void UpdateMaxHealth()
    {
        _pawnData.HealthSlider.maxValue = _pawnData.MaxHealth;
        UpdateSliderColor();
    }

    /// <summary>
    /// Updates the slider's fill color based on the amount of health the pawn has.
    /// </summary>
    private void UpdateSliderColor()
    {
        _pawnData.HealthFillImage.color = Color.Lerp(GameManager.gm.ZeroHealthColor, GameManager.gm.FullHealthColor,
            _pawnData.HealthSlider.value / _pawnData.HealthSlider.maxValue);
    }
}
