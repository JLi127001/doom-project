using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFill : MonoBehaviour
{
    [SerializeField] private BossTarget targetHealth;
    [SerializeField] private Image healthBar;
    private float redValue;
    void Update()
    {
        healthBar.fillAmount = targetHealth.health / targetHealth.maxHealth;
        if (targetHealth.invulnerable) {
            // redValue = Mathf.PingPong(Time.time, 21) * 4;
            // healthBar.color = new Color32((Byte) redValue, 0, 151, 100);
            // Debug.Log(redValue + " " + healthBar.color.r);
            healthBar.color = new Color32(0, 0, 151, 100);
        } else {
            healthBar.color = new Color32(84, 0, 151, 255);
        }
    }
}
