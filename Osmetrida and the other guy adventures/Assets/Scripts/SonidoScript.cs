using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoScript : MonoBehaviour {

    public static AudioClip golpeSteve, disparoRanged, muerteEnemigo, dañoEnemigo, muerteSteve, dañoSteve;
    static AudioSource audioSource;

	// Use this for initialization
	void Start () {

        golpeSteve = Resources.Load<AudioClip>("Wind 7 (fast)");
        disparoRanged = Resources.Load<AudioClip>("explosion_large_no_tail_05");
        muerteEnemigo = Resources.Load<AudioClip>("Generic Spell (end) 7");
        dañoEnemigo = Resources.Load<AudioClip>("Ice 8");
        muerteSteve = Resources.Load<AudioClip>("Madness 10");
        dañoSteve = Resources.Load<AudioClip>("Impact Flesh 3");

        audioSource = GetComponent<AudioSource>();

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void ejecutarSonido (string clip)
    {
        switch (clip)
        {
            case "golpeSteve":
                audioSource.PlayOneShot(golpeSteve);
                break;

            case "disparoRanged":
                audioSource.PlayOneShot(disparoRanged);
                break;

            case "muerteEnemigo":
                audioSource.PlayOneShot(muerteEnemigo);
                break;

            case "dañoEnemigo":
                audioSource.PlayOneShot(dañoEnemigo);
                break;

            case "muerteSteve":
                audioSource.PlayOneShot(muerteSteve);
                break;

            case "dañoSteve":
                audioSource.PlayOneShot(dañoSteve);
                break;

        }

    }
}
