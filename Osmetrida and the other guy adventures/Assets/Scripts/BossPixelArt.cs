using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPixelArt : MonoBehaviour {

    public Transform BallAttack;
    public float offsetx = 2f;
    public float offsety = 0.342f;

    private Vector2 posSpawn;

    public Animator animator;
    private Transform player;

    private Vector2 target;
    public float speed;

	void Start () {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
	}
	

	void Update () {

	}

    void SpawnProjectile()
    {
        posSpawn = new Vector2(gameObject.transform.position.x + offsetx, gameObject.transform.position.y + offsety);
        Instantiate(BallAttack, posSpawn, Quaternion.identity);
        posSpawn = new Vector2(gameObject.transform.position.x - offsetx, gameObject.transform.position.y + offsety);
        Instantiate(BallAttack, posSpawn, Quaternion.identity);
        offsetx += 2;
    }

    void PassToIdle()
    {
        animator.SetBool("Idle", true);
    }

    void MakeDamage()
    {
        animator.SetBool("CanDamage", true);
    }

    void StopDamage()
    {
        animator.SetBool("CanDamage", false);
    }
}
