using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionEnemy : MonoBehaviour {

    private Animator animator;

    public float hitDistance;
    public float xForce, yForce;
    public Transform player;

    public float wallColission;
    float distance = 1f;
    public float speed;

    private bool movingRight = false;
    public Transform groundDetection;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Physics2D.queriesStartInColliders = false;
    }

    void Update()
    {

        RaycastHit2D grounInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, -hitDistance);

        RaycastHit2D wallInfo = Physics2D.Raycast(groundDetection.position, transform.right, -wallColission);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (grounInfo.collider == false || (wallInfo.collider == true && wallInfo.collider.tag != "Player"))
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                movingRight = true;
            }
        }

        if (hitInfo.collider != null && hitInfo.collider.tag == "Player")
        {
            animator.SetBool("isShoot", true);
            hitInfo.collider.GetComponent<Health>().HurtPlayer(1);
            Rigidbody2D rb = hitInfo.collider.GetComponent<Rigidbody2D>();

            Vector2 force;
            Debug.Log(movingRight);
            if (movingRight)
            {
                force = Vector2.right * xForce;
            }
            else
            {
                force = Vector2.left * xForce;
            }
            rb.velocity = force;
        }
        if (hitInfo.collider == null || (hitInfo.collider != null && hitInfo.collider.tag != "Player"))
        {
            animator.SetBool("isShoot", false);
        }
    }

}
