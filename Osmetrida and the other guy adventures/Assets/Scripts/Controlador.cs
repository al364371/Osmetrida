using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour {

    public void CambiarEscena(string SampleScene){
        StartCoroutine(GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneTransition>().SceneLoad());
        }
   /* public void CambiarAContinuar(string PartidaContinuada)
    {
        print("Cambiando a la escena" + PartidaContinuada);
        SceneManager.LoadScene(PartidaContinuada);
    }
    public void CambiarAOpciones(string Opciones)
    {
        print("Cambiando a la escena" + Opciones);
        SceneManager.LoadScene(Opciones);
    }
    */
    public void Salir() {
        print("Saliendo del juego");
        Application.Quit();
        }
}