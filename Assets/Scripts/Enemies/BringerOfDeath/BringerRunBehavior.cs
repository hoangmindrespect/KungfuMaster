using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerRunBehavior : StateMachineBehaviour
{

    private BringerMovement bringer;
    private Rigidbody2D rb2D;
    [SerializeField] private float speed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bringer = animator.GetComponent<BringerMovement>();
        rb2D = bringer.rb2D;

        bringer.DetectPlayer();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       rb2D.velocity = new Vector2(speed, rb2D.velocity.y) * animator.transform.right;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       rb2D.velocity = new Vector2(0, rb2D.velocity.y);
    }
}
