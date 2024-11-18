using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    //Llamamos a la clase GameManager
    public GameObject gameManager;
    public GameObject playerData;
    
    void Awake()
    {
        //Verificamos si el GameManager sigue nulo
        if (GameManager.instance == null && PlayerData.instance == null)
        {
            Instantiate(gameManager);
            Instantiate(playerData);
        }
    }

}