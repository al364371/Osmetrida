using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConnector : MonoBehaviour {

	public int newSection;
	public float timeforActive;
	private float timeInactive;
	private BoxCollider2D theCollider;
	void Start()
	{
		theCollider = GetComponent<BoxCollider2D>();
		theCollider.enabled = false;
	}
	void Update()
	{
		if(timeInactive< timeforActive)
		{
			timeInactive += Time.deltaTime;
			if(timeInactive>= timeforActive)
			{
				theCollider.enabled = true;
			}
		}
	}
	void OnTriggerEnter2D(Collider2D collider)
	{
		StartCoroutine(LoadLevelCoroutine());
	}

	public IEnumerator LoadLevelCoroutine()
	{
		PanelEvents.TriggerFade();
		yield return new WaitForSeconds(0.3f);
		MapManagerEvents.DrawSection(newSection);
	}
}
