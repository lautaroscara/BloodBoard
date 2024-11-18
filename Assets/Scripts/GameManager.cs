using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float levelStartDelay = 1f; //Tiempo que tardara en cargar el siguiente nivel
    private Text levelText;
    private GameObject levelImage;
    public static GameManager instance = null;
    private BoardManager boardScript;//Hacemos referencia al boardmanager
    public int level = 1;//definimos el nivel de juego
    private Transform Camera;
    private bool activate;
    public GameObject merch;
    private string currentSceneName;

    void Awake()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
    }


    private void OnLevelWasLoaded()
    {
        InitGame();
    }

    //Inicializamos el juego por cada nivel
    void InitGame()
    {  
        level++;
        Debug.Log("EL NIVEL ACTUA ES: " + level);
        currentSceneName = SceneManager.GetActiveScene().name;
        if(currentSceneName == "GameScene")
        {
            levelImage = GameObject.Find("LevelImage");
            levelText = GameObject.Find("LevelText").GetComponent<Text>();
        
            if(level % 9 == 0)
            {
                MerchSpawn();
            }
            else
            {
                levelText.text = "Sala " + (level - 1);
                levelImage.SetActive(true);
            }
            boardScript.SetupScene(level);
            Invoke("HideLevelImage", levelStartDelay);
        }
    }

    public void GameOver()
    {
        level = 0;
    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
    }

    private void MerchSpawn()
    {
        levelText.text = "Sala del Mercader";
        levelImage.SetActive(true);
    }


}


