using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHands : MonoBehaviour {

    public float speed;

    private Transform player;
    private Vector2 target;

    private Animator animator;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        animator = GetComponentInParent<BossPixelArt>().animator;

    }

    // Update is called once per frame
    void Update()
    {
        target = new Vector2(player.position.x, transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        /*if (animator.GetBool("Idle"))
        { 
            target = new Vector2(player.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }*/
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (animator.GetBool("CanDamage"))
        {
            if (collider.collider.CompareTag("Player"))
            {
                collider.collider.GetComponent<Health>().HurtPlayer(1);
            }
        }
    }
}
