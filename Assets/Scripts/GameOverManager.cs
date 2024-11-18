using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Asigna el panel de Game Over desde el Inspector

    void Start()
    {
        // Asegúrate de que el panel esté oculto al inicio
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        // Pausar el juego
        Time.timeScale = 0; // Detiene el tiempo en el juego
        gameOverPanel.SetActive(true); // Muestra el panel de Game Over

        PlayerData.instance.health = PlayerData.instance.maxHealth;
        PlayerData.instance.speed = PlayerData.instance.maxSpeed;
        PlayerData.instance.hit = PlayerData.instance.maxHit;

    }

    public void HideGameOver()
    {
        // Pausar el juego
        Time.timeScale = 1; // Detiene el tiempo en el juego
        gameOverPanel.SetActive(false); // Muestra el panel de Game Over
    }


    public void RestartGame()
    {
        HideGameOver();
        // Cambia el nivel como lo mencionaste
        GameManager.instance.level = 2;
        BoardManager boardManager = FindObjectOfType<BoardManager>();
        if (boardManager != null)
        {
            boardManager.SetupScene(2);
            Debug.Log("EL NIVEL ACTUAL AL REINICIAR ES: " + GameManager.instance.level);
            
        }
    }

    public void GoToMainMenu()
    {
        GameManager.instance.GameOver();
        SceneManager.LoadScene("MainMenu"); // Cambia a la escena del menú principal
    }

    public void QuitGame()
    {
        Application.Quit(); // Cierra la aplicación
    }
}