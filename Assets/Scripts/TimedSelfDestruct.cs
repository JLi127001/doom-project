using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSelfDestruct : MonoBehaviour
{
    private float lifeTime;
    [SerializeField] private float lifeSpan;
    private void Update() {
        lifeTime += Time.deltaTime;
        if (lifeTime > lifeSpan) {
            Destroy(gameObject);
        }
    }
}
