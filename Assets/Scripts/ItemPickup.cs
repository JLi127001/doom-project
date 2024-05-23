using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isHealth;
    public bool isArmor;
    public bool isAmmo;

    public int amount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isHealth)
            {
                if (other.GetComponent<PlayerHealth>().heal(amount))
                {
                    Destroy(gameObject);
                }
            }
            if (isArmor)
            {
                if (other.GetComponent<PlayerHealth>().shield(amount))
                {
                    Destroy(gameObject);
                }
            }
            if (isAmmo)
            {
                Debug.Log("TO DO: IMPLEMENT");
            }
            
        }
    }
}
