using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    public Material aggroMat;
    public bool isAggro;
    private Transform playerTransform;
    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update() {
        if (Vector3.Distance(playerTransform.position, transform.position) <= 5f) {
            isAggro = true;
        }

        if (isAggro) {
            GetComponent<MeshRenderer>().material = aggroMat;
        }
    }
}
