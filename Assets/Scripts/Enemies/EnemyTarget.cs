using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : Target
{
    public ParticleSystem bloodParticles;
    public Transform playerTransform;
    protected virtual void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("PlayerModel").transform;
    }
    public override void TakeDamage(float damage) {
        Instantiate(bloodParticles, transform.position, Quaternion.Euler(playerTransform.position));
        base.TakeDamage(damage);
    }
}
