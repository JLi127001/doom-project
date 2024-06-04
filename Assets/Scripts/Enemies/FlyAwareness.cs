using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAwareness : MonoBehaviour
{
    public Material aggroMat;
    public Animator flyAnim;
    public bool isAggro;
    public Transform playerTransform;
    [SerializeField] private float aggroRange;
    protected virtual void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("PlayerModel").transform;
    }
    protected virtual void Update() {
        if (Vector3.Distance(playerTransform.position, transform.position) <= aggroRange) {
            flyAnim.SetBool("FlyAggravated", true);
            isAggro = true;
        } else
        {
            flyAnim.SetBool("FlyAggravated", false);
            isAggro = false;
        }

        if (isAggro) {
            GetComponent<MeshRenderer>().material = aggroMat;
        }
    }
}
