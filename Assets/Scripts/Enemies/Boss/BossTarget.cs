using System;
using UnityEngine;
public class BossTarget : Target
{
    public ParticleSystem bloodParticles;
    public ParticleSystem deathParticles;
    public ParticleSystem deathParticles2;
    public Transform playerTransform;
    [SerializeField] private BossAI bossAI;
    public float maxHealth;
    public bool invulnerable;
    public bool hitHalf;
    public FadeAway winScreen;
    protected virtual void Start() {
        invulnerable = false;
        maxHealth = health;
        playerTransform = GameObject.FindGameObjectWithTag("PlayerModel").transform;
        hitHalf = false;
    }

    public override void TakeDamage(float damage) {
        if (health - damage <= maxHealth / 2 && !hitHalf) {
            Instantiate(bloodParticles, transform.position, Quaternion.Euler(playerTransform.position));
            if (health - damage <= 0) {
                winScreen.win();
                Instantiate(deathParticles, transform.position, Quaternion.Euler(transform.up));
                Instantiate(deathParticles2, transform.position, Quaternion.Euler(transform.up));
            }
            base.TakeDamage(damage);

            bossAI.startMidPhase();
            hitHalf = true;
            invulnerable = true;
        }

        if (!invulnerable) {
            Instantiate(bloodParticles, transform.position, Quaternion.Euler(playerTransform.position));
            if (health - damage <= 0) {
                winScreen.win();
                Instantiate(deathParticles, transform.position, Quaternion.Euler(transform.up));
                Instantiate(deathParticles2, transform.position, Quaternion.Euler(transform.up));
            }
            base.TakeDamage(damage);
        }
    }
}
