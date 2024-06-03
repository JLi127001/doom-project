using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public Gun gun; 

    [SerializeField] private KeyCode reloadKey = KeyCode.R;

    private void Update()
    {
        if (Input.GetMouseButton(0) && gun.isActiveAndEnabled)
            gun.Shoot();

        if (Input.GetKeyDown(reloadKey) && gun.isActiveAndEnabled)
            gun.StartReload();
    }
}
