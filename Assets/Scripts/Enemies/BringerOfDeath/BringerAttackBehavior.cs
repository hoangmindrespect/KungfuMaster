using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerCastBehavior : StateMachineBehaviour
{
    [SerializeField] private GameObject specialSkill;
    [SerializeField] private float offsetY;
    private BringerMovement bringer;
    private Transform player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bringer = animator.GetComponent<BringerMovement>();
        player = bringer.player;

        bringer.DetectPlayer();

        Vector2 posicionAparicion = new Vector2(player.position.x, player.position.y + offsetY);

        Instantiate(specialSkill, posicionAparicion, Quaternion.identity);
    }
}
