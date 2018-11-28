using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public int health;
    public int numHearts;

    Animator animator;
    public float timeToDeath = 0.25f;

    public Image[] hearts;
    public Sprite fullHearts;
    public Sprite mediumHearts;
    public Sprite emptyHearts;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update () {
        if(health <= 0)
        {
            Muerte();
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHearts;
            }
            else
            {
                hearts[i].sprite = emptyHearts;
            }
            if(i < numHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
	}

    public void HurtPlayer(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
    }

    void Muerte()
    {
        animator.Play("Muerte");
        Destroy(gameObject, timeToDeath);
    }

    void muerte()
    {
        SonidoScript.ejecutarSonido("muerteSteve");
    }
}
