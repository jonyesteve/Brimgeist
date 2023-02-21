using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using static GameEvents;
using TMPro;

public class PlayerUIController : MonoBehaviour
{

    [SerializeField] Image playerHp;
    [SerializeField] Image playerFuel;
    [SerializeField] Image playerShield;
    [SerializeField] TextMeshProUGUI tmpText;

    private void OnEnable()
    {
        OnPlayerFuelChanged += UpdateFuel;
        OnPlayerHpChanged += UpdateHealth;
        OnPlayerShieldChanged += UpdateShieldHealth;
    }
    private void OnDisable()
    {
        OnPlayerFuelChanged -= UpdateFuel;
        OnPlayerHpChanged -= UpdateHealth;
        OnPlayerShieldChanged -= UpdateShieldHealth;
    }
    void UpdateHealth(int hp, int maxHp)
    {
        playerHp.fillAmount = (float)hp / maxHp;
    }
    void UpdateShieldHealth(int hp, int maxHp)
    {
        var tempColor = playerShield.color;
        tempColor.a = ((float)hp / maxHp) * 255;
        playerShield.color = tempColor;
        tmpText.text = $"{(int)(((float)hp / (float)maxHp)*100)}%";
    }
    void UpdateFuel(float value, float maxValue)
    {
        playerFuel.fillAmount = value / maxValue;
    }

    

}
