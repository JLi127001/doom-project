using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterEnemyAI : EnemyAI
{
    [SerializeField] private float shootRange;
    private EnemyShoot enemyShoot;
    protected override void Start() 
    {
        base.Start();
        enemyShoot = GetComponent<EnemyShoot>();
    }
    protected override void Update()
    {
        if (enemyAwareness.isAggro && 
            Vector3.Distance(playerTransform.position, transform.position) > shootRange) 
        {
            enemyNavMeshAgent.SetDestination(playerTransform.position);
        } else {
            enemyNavMeshAgent.SetDestination(transform.position);
            transform.LookAt(playerTransform);
            enemyShoot.shoot(new Vector3(0, 0, 1));
        }
    }
}
