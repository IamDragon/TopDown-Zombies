using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private FloatFloatEventSO OnPlayerTakeDamage;
    private Image healthBar;

    private void OnEnable()
    {
        OnPlayerTakeDamage.Action += UpdateHealth;
    }

    private void OnDisable()
    {
        OnPlayerTakeDamage.Action -= UpdateHealth;
    }

    private void Start()
    {
        healthBar = GetComponent<Image>();
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {

        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
