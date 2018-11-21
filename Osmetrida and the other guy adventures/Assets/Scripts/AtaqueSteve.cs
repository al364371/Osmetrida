using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueSteve : MonoBehaviour {

    private float tiempoAtaque;
    public float startTiempoAtaque;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemigos;
    public int daño;
    private bool attackFinished = true;

    private Animator animator;

    // Use this for initialization
    void Start () {

        animator = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        if (attackFinished)
        {
            if (tiempoAtaque <= 0)
            {
                if (Input.GetKeyDown(KeyCode.B))
                {

                    animator.SetBool("isAttack", true);
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemigos);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<Enemy>().vida -= daño;
                        enemiesToDamage[i].GetComponent<BossHealth>().vida -= daño;
                    }
                    attackFinished = false;
                    tiempoAtaque = startTiempoAtaque;
                }
            }
            else
            {
                tiempoAtaque -= Time.deltaTime;
            }
        }
		
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void StopAttack()
    {
        animator.SetBool("isAttack", false);
        attackFinished = true;
    }
}
