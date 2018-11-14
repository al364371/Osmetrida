using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour {

    public void CambiarEscena(string SampleScene){
        print("Cambiando a la escena" + SampleScene);
        SceneManager.LoadScene(SampleScene);
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