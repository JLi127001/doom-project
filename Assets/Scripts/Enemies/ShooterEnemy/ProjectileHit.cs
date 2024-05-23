using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
        if (other.gameObject.tag == "Player") {
            Debug.Log("Hit!");
        }
    }
}
