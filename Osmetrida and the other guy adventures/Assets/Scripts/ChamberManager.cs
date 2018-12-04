using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChamberManager : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator FadeIn()
    {
        anim.SetTrigger("end");
        yield return new WaitForSeconds(0.5f);
        //Linea para cambiar de zona de camaras
    }

    public IEnumerator FadeOut()
    {
        anim.SetTrigger("in");
        yield return new WaitForSeconds(0.5f);
        //Linea para cambiar de zona de camaras
    }
}
