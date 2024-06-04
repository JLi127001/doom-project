using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public Gun gun;
    public GameObject weaponHolder;

    private int selectedWeapon;

    [SerializeField] private KeyCode reloadKey = KeyCode.R;

    private void Update()
    {
        WeaponSwitching ws = weaponHolder.GetComponent<WeaponSwitching>();
        selectedWeapon = ws.getSelectedWeapon();
        Debug.Log(selectedWeapon);

        if (Input.GetMouseButton(0) && gun.isActiveAndEnabled) {
            gun.Shoot();
        }

        if (Input.GetKeyDown(reloadKey) && gun.isActiveAndEnabled) {
            gun.StartReload();
        }
    }
}
