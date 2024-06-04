using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Transform[] weapons;

    [Header("Keys")]
    [SerializeField] private KeyCode[] keys;

    [Header("Settings")]
    [SerializeField] private float switchTime;

    private bool[] acquiredWeapons;
    private int selectedWeapon;
    private float timeSinceLastSwitch;
    private Animator anim;

    public GameObject weaponHolder;

    public int getSelectedWeapon() {
        return this.selectedWeapon;
    }

    private void Start() {
        anim = GetComponentInParent<Animator>();
        SetWeapons();
        //Select(selectedWeapon);

        timeSinceLastSwitch = 0f;
    }

    private void SetWeapons() {
        weapons = new Transform[transform.childCount];
        acquiredWeapons = new bool[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            weapons[i] = transform.GetChild(i);
            acquiredWeapons[i] = false;
        }
            

        if (keys == null) keys = new KeyCode[weapons.Length];
    }

    private void Update() {
        int previousSelectedWeapon = selectedWeapon;

        for (int i = 0; i < keys.Length; i++)
            if (Input.GetKeyDown(keys[i]) && timeSinceLastSwitch >= switchTime && acquiredWeapons[i])
            {
                anim.SetBool("isReloading", false);
                selectedWeapon = i;
                if (previousSelectedWeapon != selectedWeapon) Select(selectedWeapon);
                timeSinceLastSwitch += Time.deltaTime;
            }
    }

    private void Select(int weaponIndex) {
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].gameObject.SetActive(i == weaponIndex);

        timeSinceLastSwitch = 0f;

        OnWeaponSelected();
    }

    private void OnWeaponSelected() {
        Gun g = weaponHolder.transform.GetChild(selectedWeapon).GetComponent<Gun>();
        CanvasManager.Instance.updateAmmo(g.getGunData().currentAmmo, g.getGunData().magSize);
    }

    public void pickedUpWeapon(GameObject weapon) {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(GameObject.ReferenceEquals(weapon, transform.GetChild(i).gameObject)){
                acquiredWeapons[i] = true;
                anim.SetBool("isReloading", false);
                selectedWeapon = i;
                Select(selectedWeapon);
                timeSinceLastSwitch += Time.deltaTime;
            }
        }
    }
}
