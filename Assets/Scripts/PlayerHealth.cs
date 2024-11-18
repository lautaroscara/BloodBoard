using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    private GameObject[] enemy;
    private GameObject board;
    private GameObject[] floor;

    public void TakeDamage(int damage)
    {
        PlayerData.instance.health -= damage;
        Debug.Log("Damage Taken: " + damage);
        Debug.Log("Current Health: " + PlayerData.instance.health);

        if (PlayerData.instance.health < 0)
        {
            PlayerData.instance.health = 0;
        }

        if (PlayerData.instance.health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObject in enemy)
        {
            Destroy(enemyObject);
        }

        // Destruye el objeto 'Board' (si no es nulo)
        board = GameObject.Find("Board");
        if (board != null)
        {
            Destroy(board);
        }

        // Encuentra todos los objetos con la etiqueta "Floor" y destrúyelos uno por uno
        floor = GameObject.FindGameObjectsWithTag("Floor");
        foreach (GameObject floorObject in floor)
        {
            Destroy(floorObject);
        }

        Destroy(gameObject);
        Debug.Log("Player Died!");
        // Aquí llamas al GameOverManager para mostrar la pantalla de Game Over
        GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }
    }

}
