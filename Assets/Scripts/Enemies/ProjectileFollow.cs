using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFollow : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float turnRadius;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate() {
        // continually add forward velocity
        GetComponent<Rigidbody>().AddForce(transform.forward.normalized * 3);

        float playerX = Math.Clamp(transform.InverseTransformPoint(player.transform.position).x, -turnRadius, turnRadius);
        float playerY = Math.Clamp(transform.InverseTransformPoint(player.transform.position).y, -turnRadius, turnRadius);

        // continually rotate towards player
        transform.Rotate(new Vector3(-playerY, 0, 0));
        transform.Rotate(new Vector3(0, playerX, 0));
    }
}
