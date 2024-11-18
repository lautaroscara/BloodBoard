using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merch : MonoBehaviour
{
    public GameObject betterHeart;
    public GameObject betterHit;
    public GameObject betterSpeed;

    BoardManager boardScript;

    private bool isPlayerNearby = false; // Detecta si el jugador está cerca
    private bool menuVisible = false; // Verifica si el menú está visible

    private PlayerData playerData;

    bool key;

    void Awake()
    {
        boardScript = gameObject.AddComponent<BoardManager>();
    }

    void Start()
    {
        if (PlayerData.instance != null)
        {
            playerData = PlayerData.instance;
        }
        else
        {
            Debug.LogError("No se encontró una instancia de PlayerData en la escena.");
        }
    }

    void Update()
    {
        // Si el jugador está cerca y presiona la tecla "L"
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.L))
        {
            Instantiate(betterHeart, new Vector3(boardScript.columns - 8, boardScript.rows - 1, 0f), Quaternion.identity);
            Instantiate(betterHit, new Vector3(boardScript.columns - 6, boardScript.rows - 1, 0f), Quaternion.identity);
            Instantiate(betterSpeed, new Vector3(boardScript.columns - 4, boardScript.rows - 1, 0f), Quaternion.identity);
            menuVisible = true;
            Debug.Log("Menú de mejoras abierto");
        }
        // Si el menú está visible, detectar las teclas 1, 2 o 3 para aplicar la mejora
        if (menuVisible)
        {
            DetectPlayerInput();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;  // Jugador está cerca
            Debug.Log("Jugador cerca del mercader");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;  // Jugador se aleja
            Debug.Log("Jugador se ha alejado del mercader");
        }
    }

    private void DetectPlayerInput()
    {
        if (menuVisible)
        {
            // Mejora fuerza
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("Mejora: Fuerza aplicada");
                ApplyUpgrade("fuerza");
                CloseMenu();
            }

            // Mejora velocidad
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("Mejora: Velocidad aplicada");
                ApplyUpgrade("velocidad");
                CloseMenu();
            }

            // Mejora vida
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("Mejora: Vida aplicada");
                ApplyUpgrade("vida");
                CloseMenu();
            }
        }
    }

    private void ApplyUpgrade(string upgradeType)
    {
        // Aquí iría la lógica para aplicar la mejora, por ejemplo, acceder a los stats del jugador
        switch (upgradeType)
        {
            case "fuerza":
                playerData.IncreaseStrength();
                break;
            case "velocidad":
                playerData.IncreaseSpeed();
                break;
            case "vida":
                playerData.IncreaseHealth();
                break;
        }
    }

    // Cierra el menú de mejoras
    private void CloseMenu()
    {
        menuVisible = false;
        betterHeart = GameObject.Find("betterHeart(Clone)");
        betterHit = GameObject.Find("betterHit(Clone)");
        betterSpeed = GameObject.Find("betterSpeed(Clone)");
        betterHeart.SetActive(false);
        betterHit.SetActive(false);
        betterSpeed.SetActive(false);
    }
}


