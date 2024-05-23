using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private float shootVel;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float fireRate;
    [SerializeField] private Transform firePoint;
    private float cooldown;

    private void Update() {
        cooldown = Math.Clamp(cooldown - Time.deltaTime, 0f, cooldown);
    }
    public void shoot(Vector3 direction) {
        if (cooldown == 0f) {
            GameObject proj = Instantiate(projectile, firePoint.position, firePoint.rotation);
            proj.transform.GetChild(0).GetComponent<Rigidbody>().AddRelativeForce(direction.normalized * shootVel);
            cooldown = fireRate;
        }
    }
}
