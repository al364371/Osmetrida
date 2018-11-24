using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    private bool movingRight = false;

    private Transform player;
    private Vector2 target;
    private Vector2 posFin;

    public GameObject explosion;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, transform.position.y);

        posFin = new Vector2(target.x + 20, transform.position.y);

	}
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (target.x == transform.position.x)
        {
            DestroyProyectile();
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
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
