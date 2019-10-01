using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("UI elements")]
    public Text healthText;

    [Header("Data Sources")]
    public HealthController playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthUI()
    {
        if (playerHealth != null)
        {
            healthText.text = "Health: " + playerHealth.healthPercent + "%";
        }
        else
        {
            healthText.text = string.Empty;
        }
    }
}
