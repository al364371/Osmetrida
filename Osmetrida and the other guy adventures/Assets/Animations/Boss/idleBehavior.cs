using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleBehavior : StateMachineBehaviour {

    private float timer;
    public float minTime;
    public float maxTime;

    private int rand;

    private Transform playerPos;
    public float speed;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        timer = Random.Range(minTime, maxTime);
        rand = Random.Range(0, 2);
	    
	}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if(timer <= 0)
        {
            if (rand == 0)
            {
                animator.SetTrigger("RangedAttack");
            }
            else
            {
                animator.SetTrigger("MeleAttack");
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }

        Vector2 target = new Vector2(playerPos.position.x, animator.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
	
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}
}
