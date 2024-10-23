using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begu_Jump : StateMachineBehaviour
{
    Rigidbody2D rb;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponentInParent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        DiveAttack();
    }
    void DiveAttack()
    {
        if(BeguGanjang.Instance.diveAttack)
        {
            BeguGanjang.Instance.Flip();

            Vector2 _newPos = Vector2.MoveTowards(rb.position, BeguGanjang.Instance.moveToPosition, BeguGanjang.Instance.speed * 2.3f * Time.fixedDeltaTime);
            rb.MovePosition(_newPos);

            float _distance = Vector2.Distance(rb.position, _newPos);
            if(_distance < 1f)
            {
                BeguGanjang.Instance.Dive();
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
