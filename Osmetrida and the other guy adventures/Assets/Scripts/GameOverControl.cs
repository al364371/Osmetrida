using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class GameOverControl : MonoBehaviour
{
    public void CambiarGameOver(string GameOver)
    {
        print("Volviendo a " + GameOver);
        SceneManager.LoadScene(GameOver);
    }
}