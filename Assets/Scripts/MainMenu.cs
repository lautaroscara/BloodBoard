using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1; // Reanuda el juego en caso de que estaba en pausa

    }

    public void Exit()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}
