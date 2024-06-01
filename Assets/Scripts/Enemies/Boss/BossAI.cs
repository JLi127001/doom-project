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
        // dashing
    [SerializeField] private Transform dashParticleTransform;
    [SerializeField] private ParticleSystem dashParticles;
    private bool particlesPlayed;
    [SerializeField] private Collider dashCollider;
    [SerializeField] private float dashVelocity;

        // shooting

    void Start()
    {
        state = 1;
    }
    private void FixedUpdate() {
        cooldown = Math.Clamp(cooldown - Time.deltaTime, 0f, cooldown);

        if (cooldown == 0) {
            state = UnityEngine.Random.Range(0, 3);
            ChangeState();
        }

        if (state == 2) {
            gameObject.transform.GetComponentInParent<Rigidbody>().AddForce(new Vector3(1, 0, 0) * dashVelocity);
        
            if (cooldown <= dashCooldown - 1 && !particlesPlayed) {
                particlesPlayed = true;
                ParticleSystem dashP = Instantiate(dashParticles, dashParticleTransform);
                dashP.transform.position = dashParticleTransform.position;
                dashP.transform.rotation = dashParticleTransform.rotation;
            }
        }
    }
    void ChangeState()
    {
        switch (state)
        {
            case 0:
                // idle
                cooldown = idleCooldown;

                // disable dashing collider
                dashCollider.gameObject.SetActive(false);

                bossAnimator.SetBool("isShooting", false);
                bossAnimator.SetBool("isDashing", false);
                break;
            case 1:
                // shooting
                cooldown = shootCooldown;

                // disable dashing collider
                dashCollider.gameObject.SetActive(false);

                bossAnimator.SetBool("isShooting", true);
                bossAnimator.SetBool("isDashing", false);
                break;
            case 2:
                // dashing
                cooldown = dashCooldown;
                particlesPlayed = false;

                // enable dashing collider
                dashCollider.gameObject.SetActive(true);

                bossAnimator.SetBool("isShooting", false);
                bossAnimator.SetBool("isDashing", true);
                break;
            default:
                // disable dashing collider
                dashCollider.gameObject.SetActive(false);

                bossAnimator.SetBool("isShooting", false);
                bossAnimator.SetBool("isDashing", false);
                break;
        }
        
    }
}
