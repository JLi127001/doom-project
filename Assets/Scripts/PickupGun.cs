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
            WeaponSwitching acquire = gun.GetComponentInParent<WeaponSwitching>();
            if(acquire != null)
            {
                acquire.pickedUpWeapon(gun);
            }
            else
            {
                Debug.Log("uh oh");
            }
            Destroy(gameObject);

        }
    }
}
