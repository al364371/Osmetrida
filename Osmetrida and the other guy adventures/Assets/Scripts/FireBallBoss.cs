using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBoss : MonoBehaviour {

    public float speed;

    private Transform player;
    private Vector2 target;

    public float animationEnd;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(transform.position.x, player.position.y);

        
	}
	
	// Update is called once per frame
	void Update () {

        if (animationEnd <= 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (target.y == transform.position.y)
            {
                DestroyProyectile();
            }
        }
        else
        {
            animationEnd -= Time.deltaTime;
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            DestroyProyectile();
            collider.GetComponent<Health>().HurtPlayer(1);
        }
    }

    void DestroyProyectile()
    {
        Destroy(gameObject);
    }
}
