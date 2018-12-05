using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyScript : MonoBehaviour {

	// Use this for initialization
	public Animator animationController;
	public int collisions;
	void Start () {
		
	}
	void Update()
	{
		if(collisions >0)
		{
			animationController.SetBool("isFly",false);
		}
		else
		{
			animationController.SetBool("isFly",true);
		}
	}
	
	// Update is called once per frame

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Ground")
		{
			collisions++;
		}

	}
	void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.tag == "Ground")
		{
			collisions--;
		}
	}
}
