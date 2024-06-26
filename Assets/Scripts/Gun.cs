using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform cam;
    private Animator anim;

    float timeSinceLastShot;

    public GunData getGunData() {
        return this.gunData;
    }

    private void Start() {
        anim = GetComponentInParent<Animator>();
        gunData.reloading = false;
        gunData.currentAmmo = gunData.magSize;
        CanvasManager.Instance.updateAmmo(gunData.currentAmmo, gunData.magSize);
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload() {
        if (!gunData.reloading && this.gameObject.activeSelf)
            StartCoroutine(Reload());
    }

    private IEnumerator Reload() {
        gunData.reloading = true;
        anim.SetBool("isReloading", true);
        CanvasManager.Instance.updateAmmo("reloading");

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        anim.SetBool("isReloading", false);
        gunData.reloading = false;
        CanvasManager.Instance.updateAmmo(gunData.currentAmmo, gunData.magSize); // replace magsize with ammo inventory
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    public void Shoot() {
        if (gunData.currentAmmo > 0) {
            if (CanShoot()) {
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance)){
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);
                }
                anim.Play("GunShoot");

                gunData.currentAmmo--;
                CanvasManager.Instance.updateAmmo(gunData.currentAmmo, gunData.magSize);
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
    }

    private void Update() {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(cam.position, cam.forward * gunData.maxDistance);
    }

    private void OnGunShot() {  }
}
