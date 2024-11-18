using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    // Usamos serializable para vizualizar los elementos en el inspector
    [SerializeField]
    //Creamos una función para establecer la cantidad de objetos que se instanciaran
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    //Establecemos la cantidad de filas y columnas del mapa
    public int columns = 16;
    public int rows = 8;

    //Establecemos un maximo y minimo de paredes y comida que aparecera
    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);

    //Establecemos los prefabs que apareceran en escena
    public GameObject player;
    public GameObject exit;
    private GameObject playerInstance;
    private Vector3 playerInstantiate;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] cornerTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;
    public bool enemyEnabled = true;
    public GameObject merch;

    //Creamos una variable para guardar todos los gameobjects que se crearan
    private Transform boardHolder;

    //Una lista de la localización de cada carrilla
    private List<Vector3> gridPositions = new List<Vector3>();
    public int enemysInstantiated; //Contador de enemigos vivos

    //Iniciamos la lista para dejarla preparada y cargar el mapa
    void InitialiseList()
    {
        gridPositions.Clear();
        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    //Instanciamos los pisos y las paredes del borde
    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        Vector3 newpos = new Vector3(9, 4 , 0);
        boardHolder.transform.position = newpos;
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                /*
                GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];
                if(x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];
                GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent (boardHolder);
                */
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == columns || x == -1)
                    toInstantiate = outerWallTiles[Random.Range(6, 10)];
                else
                {
                    if (y == rows || y == -1)
                    {
                        if (rows == -1 && columns == 8)
                            toInstantiate = cornerTiles[0];
                        else
                        {
                            if (rows == 20 && columns == 8)
                                toInstantiate = cornerTiles[3];
                            else
                            {
                                toInstantiate = outerWallTiles[Random.Range(0, 5)];
                            }
                        }

                    }
                }
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }

    }

    //Una función para establecer posiciones aleatorias en el mapa
    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    //Guarda un array de GameObjects para elegir a lo largo del mismo, con un maximo y un minimo de objetos a crear
    void LayoutObjectAtRandom(GameObject[] tileArray, int maximum)
    {
        for (int i = 0; i < maximum; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length - 1)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    void LayoutEnemysAtRandom(GameObject[] enemyArray, int maximum, Transform playerTransform)
    {
        for (int i = 0; i < maximum; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject enemyInstance = Instantiate(enemyArray[Random.Range(0, enemyArray.Length)], randomPosition, Quaternion.identity);

            Enemy enemy = enemyInstance.GetComponent<Enemy>();
            EnemyHealth enemyHealth = enemyInstance.GetComponent<EnemyHealth>();
            if (enemy != null && enemyHealth != null)
            {
                enemy.SetPlayer(playerTransform);
                enemyHealth.SetBoardManager(this); // Aquí se enlaza el BoardManager
            }
        }
    }

    public void DisabledEnemies()
    {
        enemyEnabled = false;
    }

    private IEnumerator DelayEnemyInstance(float delay, int level, Transform playerInstance)
    {
        yield return new WaitForSeconds(delay);
        try
        {    
            int enemyCount = (int)Mathf.Log(level, 2f);
            LayoutEnemysAtRandom(enemyTiles, enemyCount, playerInstance.transform);
            enemysInstantiated = enemyCount;
        }
        catch(MissingReferenceException ex)
        {
            Debug.LogWarning("Excepción capturada: " + ex.Message);
        }
    }

    public void EnemyKilled()
    {
        if (enemysInstantiated <= 0)
        {
            Instantiate(exit, new Vector3(columns - 1, rows - 5, 0f), Quaternion.identity);
        }
    }

     private void MerchBoard()
    {
        DisabledEnemies();
        Instantiate(merch, new Vector3(columns / 2, rows / 2, 0f), Quaternion.identity);
        playerInstantiate = new Vector3(columns / 5, rows / 3, 0f);
        playerInstance = Instantiate(player, playerInstantiate, Quaternion.identity);
        Instantiate(exit, new Vector3(columns - 1, rows/2, 0f), Quaternion.identity);
    }

    //Inicializamos el nivel cargamos los metodos
    public void SetupScene(int level)
    {
        if (level % 9 == 0)
        {
            // Este es el nivel de mercader (divisible por 8)
            BoardSetup();
            InitialiseList();
            MerchBoard();
        }
        else
        {
            // Otros niveles
            BoardSetup();
            InitialiseList();
            playerInstantiate = new Vector3(columns / 2, rows / 2, 0f);
            playerInstance = Instantiate(player, playerInstantiate, Quaternion.identity);
            StartCoroutine(DelayEnemyInstance(2.5f, level, playerInstance.transform));
            LayoutObjectAtRandom(wallTiles, wallCount.maximum);
        }
    }
}