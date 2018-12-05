using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour {

	// Use this for initialization
	public Animator panelAnimator;
	void Start () {
		DontDestroyOnLoad(this.gameObject);
		PanelEvents.FadeEventCollector += Fader;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Fader()
	{
		StartCoroutine(DestructCoroutine());
	}

	public IEnumerator DestructCoroutine()
	{
		panelAnimator.SetTrigger("end");
		yield return null;
		panelAnimator.SetTrigger("in");
		yield return new WaitForSeconds(0.2f);
		panelAnimator.SetTrigger("in");
	}
}

public static class PanelEvents
{
	public delegate void FadeEvent();
	public static event FadeEvent FadeEventCollector;
	public static void TriggerFade()
	{
		FadeEventCollector();
	}
}
