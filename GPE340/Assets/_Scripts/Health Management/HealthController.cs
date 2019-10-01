using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class HealthController : MonoBehaviour
{
    [Header("Health Values")] public float currentHealth;

    [SerializeField] private float maxHealth;

    // Helper functions
    public float healthPercent
    {
        get { return (currentHealth / maxHealth) * 100; }
        set { currentHealth = maxHealth * value; }
    }

    [Header("UnityEvents")]
    public UnityEvent OnDeathEvent;
    public UnityEvent OnChangeHealth;


    // Start is called before the first frame update
    void Start()
    {
        HealToFull();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageTaken, Pawn source = null)
    {
        currentHealth = Mathf.Clamp(currentHealth - damageTaken, 0, maxHealth);
        OnChangeHealth.Invoke();

        if (source != null)
        {
            Debug.Log("Shot by: " + source.name);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float healingTaken)
    {
        healthPercent = Mathf.Clamp(currentHealth + healingTaken, 0, maxHealth);
        OnChangeHealth.Invoke();
    }

    private void Die()
    {
        // Whatever needs to be done when you die
        SendMessage("OnDie");
    }

    public void KillMe()
    {
        currentHealth = 0;
        OnChangeHealth.Invoke();
        Die();
    }

    public void HealToFull()
    {
        currentHealth = maxHealth;
        OnChangeHealth.Invoke();
    }
}
