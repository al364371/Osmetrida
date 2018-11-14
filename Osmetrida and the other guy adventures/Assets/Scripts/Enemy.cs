using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    public int vida;
    public Animator animator;
    public float timeBfrDie = 1.0f;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (vida == 0)
        {
            animator.Play("Die");
            Destroy(gameObject, timeBfrDie);
        }

    }
}
