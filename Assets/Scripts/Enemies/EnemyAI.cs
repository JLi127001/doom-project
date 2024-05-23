using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public EnemyAwareness enemyAwareness;
    public Transform playerTransform;
    public NavMeshAgent enemyNavMeshAgent;
    protected virtual void Start() {
        enemyAwareness = GetComponent<EnemyAwareness>();
        playerTransform = GameObject.FindGameObjectWithTag("PlayerModel").transform;
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
    }
    protected virtual void Update() {
        if (enemyAwareness.isAggro) {
            enemyNavMeshAgent.SetDestination(playerTransform.position);
        } else {
            enemyNavMeshAgent.SetDestination(transform.position);
        }
    }
}
