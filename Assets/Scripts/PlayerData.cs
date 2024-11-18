using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    public float health;

    public float speed;

    public float hit;

    public float maxHealth;

    public float maxSpeed;

    public float maxHit;
    

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        health = 6f;
        speed = 5.5f;
        hit = 3.45f;
        maxHealth = 6f;
        maxSpeed = 5.5f;
        maxHit = 3.45f;
    }

    public void IncreaseSpeed()
    {
        speed = speed * 1.90f; // Incrementa la velocidad
        maxSpeed = maxSpeed * 1.90f; 
        Debug.Log("Velocidad aumentada a: " + speed);
    }

    public void IncreaseHealth()
    {
        health = 6f + 1f; // Incrementa la salud
        maxHealth = 6f + 1f; 
        Debug.Log("Salud aumentada a: " + health);
    }

    public void IncreaseStrength()
    {
        hit = hit * 1.80f; // Incrementa la fuerza
        maxHit = maxHit * 1.80f; 
        Debug.Log("Fuerza aumentada a: " + hit);
    }

}
