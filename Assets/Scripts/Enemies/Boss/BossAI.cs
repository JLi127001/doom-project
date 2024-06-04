using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    // state changes
    private float cooldown;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float idleCooldown;
    public int state; // 0 = idle, 1 = shooting, 2 = dashing
    public bool midPhase;

    // animator
    public Animator bossAnimator;

    // attacking
    private GameObject player;
        // dashing
    [SerializeField] private Transform dashParticleTransform;
    [SerializeField] private ParticleSystem dashParticles;
    private bool particlesPlayed;
    // [SerializeField] private Collider dashCollider;
    [SerializeField] private float dashVelocity;

        // shooting
    [SerializeField] GameObject bossProjectile;
    [SerializeField] private Transform projectileSpawnTransform;

    // misc
    private Vector3 originalPos;
    private Rigidbody parentRigBod;
    private Transform parentTransform;
    [SerializeField] private BossTarget bossTarget;

    // mid phase
    [SerializeField] GameObject midPhaseEnemyHolder;
    private GameObject[] bossEnemies;

    void Start()
    {
        parentRigBod = transform.GetComponentInParent<Rigidbody>();
        parentTransform = transform.GetComponentInParent<Transform>();
        originalPos = parentRigBod.transform.position;
        state = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        midPhase = false;
        bossEnemies = GameObject.FindGameObjectsWithTag("BossMinion");
    }

    private void FixedUpdate() {
        cooldown = Math.Clamp(cooldown - Time.deltaTime, 0f, cooldown);
        if (parentRigBod.transform.position.y < originalPos.y + -20) {
            parentRigBod.transform.position = originalPos;
            parentRigBod.velocity = Vector3.zero;
        } else if (parentRigBod.transform.position.y > originalPos.y + 40) {
            parentRigBod.AddForce(new Vector3(0, -5, 0));
        }

        // state change when attack cooldown hits 0
        if (cooldown == 0 && !midPhase) {
            // if state is already idle, change it to a random state between 1-2
            // else, change to idle
            if (state == 0) {
                state = UnityEngine.Random.Range(1, 3);
                ChangeState();
            } else {
                state = 0;
                ChangeState();
            }
        } else if (midPhase) {
            // if the boss is in it's mid phase, keep it in idle
            state = 0;
            ChangeState();
        }

        // idle state, boss should rotate towards player
        if (state == 0) {
            // GetComponentInParent<Rigidbody>() = Quaternion.Euler(0, GetComponentInParent<Rigidbody>().rotation.y, 0);
            // get player position
            float playerZ = Math.Clamp(transform.InverseTransformPoint(player.transform.position).z, -1, 1);
            // continually rotate towards player
            if (playerZ >= 5 || playerZ <= 5) {
                parentRigBod.transform.Rotate(new Vector3(0, -playerZ, 0));
            }
        }

        // shooting state
        if (state == 1) {
            // not needed in update so far
        }

        // dash attack state, boss should be moving forward after a slight delay
        if (state == 2) {
            // emit particles
            if (cooldown <= dashCooldown - .5 && !particlesPlayed) {
                particlesPlayed = true;
                ParticleSystem dashP = Instantiate(dashParticles, dashParticleTransform);
                dashP.transform.position = dashParticleTransform.position;
                dashP.transform.rotation = dashParticleTransform.rotation;
            }
            
            // move forward
            if (cooldown > dashCooldown - 1) {
                parentRigBod.AddRelativeForce(transform.InverseTransformPoint(dashParticleTransform.position).normalized * dashVelocity);
            }
        }

        if (midPhase) {
            bossEnemies = GameObject.FindGameObjectsWithTag("BossMinion");
            if (bossEnemies.Length == 0) {
                midPhase = false;
                bossTarget.invulnerable = false;
            }
        }
    }

    private void shoot() {
        Instantiate(bossProjectile, projectileSpawnTransform.position, projectileSpawnTransform.rotation);
    }

    public void startMidPhase() {
        midPhaseEnemyHolder.SetActive(true);
        midPhase = true;
    }

    void ChangeState()
    {
        switch (state)
        {
            case 0:
                parentRigBod.rotation = new Quaternion(0, parentRigBod.rotation.y, 0, parentRigBod.rotation.w).normalized;

                // idle
                cooldown = idleCooldown;

                // disable dashing collider
                // (collider has script to damage on interval on player contact)
                // dashCollider.gameObject.SetActive(false);

                // disable shooting
                CancelInvoke();

                bossAnimator.SetBool("isShooting", false);
                bossAnimator.SetBool("isDashing", false);
                break;
            case 1:
                parentRigBod.rotation = new Quaternion(0, parentRigBod.rotation.y, 0, parentRigBod.rotation.w).normalized;

                // shooting
                cooldown = shootCooldown;

                // start shooting
                InvokeRepeating("shoot", .5f, 1f);

                // disable dashing collider
                // dashCollider.gameObject.SetActive(false);

                bossAnimator.SetBool("isShooting", true);
                bossAnimator.SetBool("isDashing", false);
                break;
            case 2:
                parentRigBod.rotation = new Quaternion(0, parentRigBod.rotation.y, 0, parentRigBod.rotation.w).normalized;

                // dashing
                cooldown = dashCooldown;
                particlesPlayed = false;

                // disable shooting
                CancelInvoke();

                // enable dashing collider
                // dashCollider.gameObject.SetActive(true);

                bossAnimator.SetBool("isShooting", false);
                bossAnimator.SetBool("isDashing", true);
                break;
            default:
                parentRigBod.rotation = new Quaternion(0, parentRigBod.rotation.y, 0, parentRigBod.rotation.w).normalized;
            
                // disable dashing collider
                // dashCollider.gameObject.SetActive(false);

                // disable shooting
                CancelInvoke();

                bossAnimator.SetBool("isShooting", false);
                bossAnimator.SetBool("isDashing", false);
                break;
        }
    }
}
