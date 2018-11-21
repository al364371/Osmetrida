using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPixelArt : MonoBehaviour {

    public Transform BallAttack;
    public float offsetx = 3f;
    public float offsety = 200f;

    private Vector2 posSpawn;

    public Animator animator;
    private Transform player;

    private Vector2 target;
    public float speed;

    public Slider healthBar;

	void Start () {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
	}
	

	void Update () {

        healthBar.value = gameObject.GetComponent<Enemy>().vida;

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
