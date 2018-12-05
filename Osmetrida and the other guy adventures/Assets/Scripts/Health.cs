using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public int health;
    public int numHearts;

    Animator animator;
    public float timeToDeath = 0.25f;
    public float invincibilityTime = 0.5f;
    private float timeInvincible = 0f;
    public SpriteRenderer image;
    private bool invincible;
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
        if(invincible)
        {
            image.enabled = !image.enabled;
            timeInvincible += Time.deltaTime;
            if(timeInvincible >= invincibilityTime)
            {
                timeInvincible = 0;
                invincible = false;
                image.enabled = true;
            }
        }
	}

    public void HurtPlayer(int damage)
    {
        if(!invincible)
        {
            health -= damage;
            if (health < 0)
            {
                health = 0;
            }
            invincible = true;
        }
    }

    void Muerte()
    {
        animator.Play("Muerte");
        StartCoroutine(GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneTransition>().SceneLoad());
    }

    void muerte()
    {
        SonidoScript.ejecutarSonido("muerteSteve");
    }
}
