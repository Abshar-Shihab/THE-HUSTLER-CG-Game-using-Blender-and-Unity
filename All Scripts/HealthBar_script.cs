using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_script : MonoBehaviour
{
    private Image HealthBar;
    public float CurrentHealth;
    private float maxHealth = 100f;
    player_movement player;

    private void Start()
    {
        HealthBar = GetComponent<Image>();
        player = FindObjectOfType<player_movement>();
    }

    private void Update()
    {
        CurrentHealth = player.health;
        HealthBar.fillAmount = CurrentHealth / maxHealth;
    }
}
