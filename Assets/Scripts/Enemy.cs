using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyManager enemyManager;
    private float enemyHealth = 2f;
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
        enemyHealth -= damage;
    }
}
