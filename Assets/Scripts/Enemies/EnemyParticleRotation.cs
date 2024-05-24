using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleRotation : MonoBehaviour
{
    void Start()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("PlayerModel").transform);
    }
}
