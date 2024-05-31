using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{

    public Animator bossAnimator;
    public int state; // 0 = idle, 1 = shooting, 2 = dashing
    void Start()
    {
        state = 1;
        //Change animation every 3 seconds
        InvokeRepeating("ChangeAnimation", 3.0f, 3.0f);
    }
    void ChangeAnimation()
    {
        //set state (please change this, just for demonstration on how to use animator)
        state = (state + 1) % 3;

        switch (state)
        {
            case 0:
                bossAnimator.SetBool("isShooting", false);
                bossAnimator.SetBool("isDashing", false);
                break;
            case 1:
                bossAnimator.SetBool("isShooting", true);
                bossAnimator.SetBool("isDashing", false);
                break;
            case 2:
                bossAnimator.SetBool("isShooting", false);
                bossAnimator.SetBool("isDashing", true);
                break;
            default:
                bossAnimator.SetBool("isShooting", false);
                bossAnimator.SetBool("isDashing", false);
                break;
        }
        
    }
}
