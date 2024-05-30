using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    [SerializeField] float damage;
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "PlayerModel") {
            other.gameObject.GetComponentInParent<PlayerHealth>().damagePlayer((int) damage);
        }
        Destroy(gameObject);
    }
}
