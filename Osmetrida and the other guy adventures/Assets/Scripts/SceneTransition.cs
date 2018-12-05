using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

    public Animator anim;

    public string sceneName;
    void Start()
    {
        TransitorEvents.TransitEvent += TransitToBoss;
    }

    public void TransitToBoss()
    {
        sceneName = "BossFight1";
        StartCoroutine(SceneLoad());
    }
    public IEnumerator SceneLoad()
    {
        anim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}

public static class TransitorEvents
{
    public delegate void TransitHandler();
    public static event TransitHandler TransitEvent;
    public static void TriggerTransit()
    {
        TransitEvent();
    }
}
