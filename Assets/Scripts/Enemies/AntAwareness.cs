using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntAwareness : MonoBehaviour
{
    public Material aggroMat;
    public Animator antAnim;
    public bool isAggro;
    public Transform playerTransform;
    [SerializeField] private float aggroRange;
    protected virtual void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("PlayerModel").transform;
    }
    protected virtual void Update() {
        if (Vector3.Distance(playerTransform.position, transform.position) <= aggroRange) {
            antAnim.SetBool("AntAggravated", true);
            isAggro = true;
        } else
        {
            antAnim.SetBool("AntAggravated", false);
            isAggro = false;
        }

        if (isAggro) {
            GetComponent<MeshRenderer>().material = aggroMat;
        }
    }
}
