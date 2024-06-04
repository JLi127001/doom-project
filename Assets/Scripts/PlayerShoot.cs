using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public GameObject weaponHolder;
    private int selectedWeapon;

    [SerializeField] private KeyCode reloadKey = KeyCode.R;

    private void Update()
    {
        WeaponSwitching ws = weaponHolder.GetComponent<WeaponSwitching>();
        selectedWeapon = ws.getSelectedWeapon();

        // NOTE: This is somewhat hacky, requires specific orderings in the hierarchy!
        Gun g = weaponHolder.transform.GetChild(selectedWeapon).GetChild(0).GetComponent<Gun>();

        if (Input.GetMouseButton(0) && g.isActiveAndEnabled) {
            g.Shoot();
        }

        if (Input.GetKeyDown(reloadKey) && g.isActiveAndEnabled) {
            g.StartReload();
        }
    }
}
