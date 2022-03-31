using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthInfoUI : MonoBehaviour
{

    public TextMeshProUGUI healthText;

    public TextMeshProUGUI maxHealthText;

    private HealthComponent playerHealthComponent;

    private void OnEnable()
    {
        PlayerEvents.OnHealthInitialized += OnHealthInitialized;
    }

    private void OnDisable()
    {
        PlayerEvents.OnHealthInitialized -= OnHealthInitialized;
    }
    private void OnHealthInitialized(HealthComponent healthComponent)
    {
        playerHealthComponent = healthComponent;
    }

    void Update()
    {
        healthText.text = playerHealthComponent.CurrentHealth.ToString();
        maxHealthText.text = playerHealthComponent.MaxHealth.ToString();
    }
}
