using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGun : MonoBehaviour
{

    public GameObject gun;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gun.SetActive(true);
            Destroy(gameObject);

        }
    }
}
