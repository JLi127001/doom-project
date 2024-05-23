using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyManager enemyManager;
    public float enemyHealth = 2f;

    public GameObject gunHitEffect;

    private void Start() {
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
    }
    private void Update() {
        if (enemyHealth <= 0) {
            enemyManager.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
    
    private void TakeDamage(float damage) {
        Instantiate(gunHitEffect, transform.position, Quaternion.identity);
        enemyHealth -= damage;
    }
}
