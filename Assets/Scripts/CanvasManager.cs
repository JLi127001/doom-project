using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI armor;
    public TextMeshProUGUI ammo;

    private static CanvasManager _instance;

    public static CanvasManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void updateHealth(int val)
    {
        health.text = "<Health> " + val.ToString();

    }
    public void updateArmor(int val)
    {
        armor.text = "<Armor> " + val.ToString();
    }
    public void updateAmmo(int val)
    {
        ammo.text = "<Ammo> " + val.ToString();
    }
}
