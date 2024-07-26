using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAnimationController : MonoBehaviour
{
    public Animator animator;
    public void DisableAnimator()
    {
        if(animator.enabled == true)
        {
            FindObjectOfType<EnemyRemaining>().EnemyCounter();
        }
        animator.enabled = false;
        
    }
}
