using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    public Material aggroMat;
    public bool isAggro;
    public Transform playerTransform;
    [SerializeField] private float aggroRange;
    protected virtual void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected virtual void Update() {
        if (Vector3.Distance(playerTransform.position, transform.position) <= aggroRange) {
            isAggro = true;
        }

        if (isAggro) {
            GetComponent<MeshRenderer>().material = aggroMat;
        }
    }
}
