using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorPausa : MonoBehaviour {

	public void CambiarEscena(string LevelGeneratorScene)
    {
        print("Cambiando a la escena" + LevelGeneratorScene);
        SceneManager.LoadScene(LevelGeneratorScene);
    }

    public void CambiarEscena2(string Pantallaprincipal)
    {
        print("Cambiando a la escena" + Pantallaprincipal);
        GameObject.FindGameObjectWithTag("pause").GetComponent<pause>().active = true;
        Time.timeScale = (GameObject.FindGameObjectWithTag("pause").GetComponent<pause>().active) ? 1f : 0;
        SceneManager.LoadScene(Pantallaprincipal);
    }

}
