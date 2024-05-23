using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterEnemyAI : EnemyAI
{
    [SerializeField] private float shootRange;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private LayerMask projLayer;
    private EnemyShoot enemyShoot;
    protected override void Start() 
    {
        base.Start();
        enemyShoot = GetComponent<EnemyShoot>();
    }
    protected override void Update()
    {
        // Detect if player is in line of sight from the shooting point
        bool playerInLOS = false;
        if (Physics.Raycast(shootPoint.position, playerTransform.position - shootPoint.position, out RaycastHit hitInfo, shootRange, ~projLayer)) {
            if (hitInfo.collider.tag == "PlayerModel") playerInLOS = true;
        }

        if (enemyAwareness.isAggro && 
            (Vector3.Distance(playerTransform.position, transform.position) > shootRange ||
            (Vector3.Distance(playerTransform.position, transform.position) <= shootRange && !playerInLOS))) 
        {
            enemyNavMeshAgent.SetDestination(playerTransform.position);
        } else {
            enemyNavMeshAgent.SetDestination(transform.position);
            if (enemyAwareness.isAggro && Vector3.Distance(playerTransform.position, transform.position) <= shootRange) {
                transform.LookAt(playerTransform);
                enemyShoot.shoot(new Vector3(0, 0, 1));
            }
        }
    }
}
