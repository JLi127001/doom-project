using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Animator doorAnim;
    public GameObject keycard;
    public GameObject spawnEnemies;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !keycard.activeSelf)
        {
            doorAnim.SetTrigger("OpenDoor");
            spawnEnemies.SetActive(true); 
        }
    }
}
