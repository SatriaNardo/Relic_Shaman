using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begu_Walk : StateMachineBehaviour
{
    Rigidbody2D rb;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponentInParent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TargetPlayerPosition(animator);

        if (BeguGanjang.Instance.attackCountdown <= 0)
        {
            BeguGanjang.Instance.AttackHandler();
            BeguGanjang.Instance.attackCountdown = Random.Range(BeguGanjang.Instance.attackTimer - 1, BeguGanjang.Instance.attackTimer + 1);
        }
    }
    void TargetPlayerPosition(Animator animator)
    {
        if(BeguGanjang.Instance.Grounded())
        {
            BeguGanjang.Instance.Flip();
            Vector2 _target = new Vector2(PlayerController.Instance.transform.position.x, rb.position.y);
            Vector2 _newPos = Vector2.MoveTowards(rb.position, _target, BeguGanjang.Instance.runSpeed * Time.fixedDeltaTime);
            BeguGanjang.Instance.runSpeed = BeguGanjang.Instance.speed;
            rb.MovePosition(_newPos);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -25);
        }
        if(Vector2.Distance(PlayerController.Instance.transform.position, rb.position) <= BeguGanjang.Instance.attackRange)
        {
            animator.SetBool("Walk", false);
        }
        else
        {
            return;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Walk", false);
    }

}
