using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float hitRate;
    private float cooldown;
    private void Update() {
        cooldown = Math.Clamp(cooldown - Time.deltaTime, 0f, cooldown);
    }
    private void OnCollisionEnter(Collision other) {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "PlayerModel" && cooldown == 0f) {
            cooldown = hitRate;
            other.gameObject.GetComponentInParent<PlayerHealth>().damagePlayer((int) damage);
        }
    }
}

