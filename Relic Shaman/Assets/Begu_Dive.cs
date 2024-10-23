using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begu_Dive : StateMachineBehaviour
{
    Rigidbody2D rb;
    bool callOnce;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponentInParent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BeguGanjang.Instance.divingCollider.SetActive(true);

        if(BeguGanjang.Instance.Grounded())
        {
            BeguGanjang.Instance.divingCollider.SetActive(false);
            if(!callOnce)
            {
                BeguGanjang.Instance.DivingPillars();
                animator.SetBool("Dive", false);
                BeguGanjang.Instance.ResetAllAttacks();
                callOnce = true;
            }
            
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        callOnce = false;
    }
}
