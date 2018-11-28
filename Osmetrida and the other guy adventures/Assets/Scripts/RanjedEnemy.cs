using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanjedEnemy : MonoBehaviour
{
    private Animator animator;

    public float shootDistance;
    public Transform player;

    public float wallColission;
    float distance = 1f;
    public float speed;

    private bool movingRight = false;
    public Transform groundDetection;

    private float timeBtwShoots;
    public float startTimeBtwShoots;
    public Transform projectile;

    public Transform steveDetection;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Physics2D.queriesStartInColliders = false;
    }

    void Update () {

        RaycastHit2D grounInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);

        RaycastHit2D hitInfo = Physics2D.Raycast(steveDetection.position, transform.right, -shootDistance);

        RaycastHit2D wallInfo = Physics2D.Raycast(groundDetection.position, transform.right, -wallColission);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            timeBtwShoots = 0.15f;
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

            if (timeBtwShoots <= 0)
            {
                SonidoScript.ejecutarSonido("disparoRanged");

                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShoots = startTimeBtwShoots;
            }

            else
            {
                timeBtwShoots -= Time.deltaTime;
            }
        }
        if (hitInfo.collider == null || (hitInfo.collider != null && hitInfo.collider.tag != "Player"))
        {
            animator.SetBool("isShoot", false);
        }
    }
}
