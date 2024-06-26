using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    public string AnimatorString;
    public Animator anim;
    public bool isAggro;
    public Transform playerTransform;
    [SerializeField] private float aggroRange;
    protected virtual void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("PlayerModel").transform;
    }
    protected virtual void Update() {
        if (Vector3.Distance(playerTransform.position, transform.position) <= aggroRange) {
            isAggro = true;
        }

        if (isAggro) {
            anim.SetBool(AnimatorString, true);
        }
    }
}
