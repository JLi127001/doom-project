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

    // animator
    public Animator bossAnimator;

    // attacking
    private GameObject player;
        // dashing
    [SerializeField] private Transform dashParticleTransform;
    [SerializeField] private ParticleSystem dashParticles;
    private bool particlesPlayed;
    [SerializeField] private Collider dashCollider;
    [SerializeField] private float dashVelocity;

        // shooting
    [SerializeField] GameObject bossProjectile;
    [SerializeField] private Transform projectileSpawnTransform;

    void Start()
    {
        state = 1;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate() {
        cooldown = Math.Clamp(cooldown - Time.deltaTime, 0f, cooldown);

        // state change when attack cooldown hits 0
        if (cooldown == 0) {
            // if state is already idle, change it to a random state between 1-2
            // else, change to idle
            if (state == 0) {
                state = UnityEngine.Random.Range(1, 3);
                ChangeState();
            } else {
                state = 0;
                ChangeState();
            }
        }

        // idle state, boss should rotate towards player
        if (state == 0) {
            // GetComponentInParent<Rigidbody>() = Quaternion.Euler(0, GetComponentInParent<Rigidbody>().rotation.y, 0);
            // get player position
            float playerZ = Math.Clamp(transform.InverseTransformPoint(player.transform.position).z, -1, 1);
            // continually rotate towards player
            if (playerZ >= 5 || playerZ <= 5) {
                transform.GetComponentInParent<Transform>().transform.Rotate(new Vector3(0, -playerZ, 0));
            }
        }

        // shooting state
        if (state == 1) {
            // not needed in update so far
        }

        // dash attack state, boss should be moving forward after a slight delay
        if (state == 2) {
            // emit particles
            if (cooldown <= dashCooldown - 1 && !particlesPlayed) {
                particlesPlayed = true;
                ParticleSystem dashP = Instantiate(dashParticles, dashParticleTransform);
                dashP.transform.position = dashParticleTransform.position;
                dashP.transform.rotation = dashParticleTransform.rotation;
            }
            
            // move forward
            if (cooldown > dashCooldown - 1) {
                transform.GetComponentInParent<Transform>().position += transform.InverseTransformPoint(dashParticleTransform.position).normalized;
            }
        }
    }

    private void shoot() {
        Instantiate(bossProjectile, projectileSpawnTransform.position, projectileSpawnTransform.rotation);
    }

    void ChangeState()
    {
        switch (state)
        {
            case 0:
                // idle
                cooldown = idleCooldown;

                // disable dashing collider
                // (collider has script to damage on interval on player contact)
                dashCollider.gameObject.SetActive(false);

                // disable shooting
                CancelInvoke();

                bossAnimator.SetBool("isShooting", false);
                bossAnimator.SetBool("isDashing", false);
                break;
            case 1:
                // shooting
                cooldown = shootCooldown;

                // start shooting
                InvokeRepeating("shoot", .5f, 1f);

                // disable dashing collider
                dashCollider.gameObject.SetActive(false);

                bossAnimator.SetBool("isShooting", true);
                bossAnimator.SetBool("isDashing", false);
                break;
            case 2:
                // dashing
                cooldown = dashCooldown;
                particlesPlayed = false;

                // disable shooting
                CancelInvoke();

                // enable dashing collider
                dashCollider.gameObject.SetActive(true);

                bossAnimator.SetBool("isShooting", false);
                bossAnimator.SetBool("isDashing", true);
                break;
            default:
                // disable dashing collider
                dashCollider.gameObject.SetActive(false);

                // disable shooting
                CancelInvoke();

                bossAnimator.SetBool("isShooting", false);
                bossAnimator.SetBool("isDashing", false);
                break;
        }
        
    }
}
