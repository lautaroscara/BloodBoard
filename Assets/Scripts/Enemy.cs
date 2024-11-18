using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Componentes externos
    [SerializeField] private LayerMask playerLayer;
    
    //Movimiento del Enemigo
    private Transform player;
    private float detectionRadius = 5f;
    public float speed = 1.89f;
    private Vector2 randomTarget;
    private bool isPlayerDetected = false;

    //Componentes del enemigo
    private Animator animator;
    private Rigidbody2D rb;

    //Ataque
    private EnemyAttack enemyAttack;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyAttack = GetComponent<EnemyAttack>();
        if (enemyAttack != null)
        {
            enemyAttack.SetPlayer(player);
        }
    }

    void Update()
    {
        if(!isPlayerDetected)
            DetectPlayer();

        if (isPlayerDetected)
            Follow();
        else
            MoveRandomly();
        
            
    }

    //Detecta al jugador dentro de cierta area
    private void DetectPlayer()
    {
        Collider2D detectedPlayer = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        if (detectedPlayer != null)
        {
            isPlayerDetected = true;
        }
    }

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
        if(enemyAttack != null)
        {
            enemyAttack.SetPlayer(playerTransform);
        }
    }

    //Mueve el enemigo a la posici�n del jugador siguiendolo
    private void Follow()
    {       
       Vector2 direction = (player.position - transform.position).normalized;
       Vector2 newPosition = (Vector2)transform.position + (direction * speed * Time.deltaTime);
       rb.MovePosition(newPosition); 
    }

    //Mueve al enemigo a una posici�n aleatoria
    private void MoveRandomly()
    {
        if (Vector2.Distance(transform.position, randomTarget) < 0.1f)
        {
            SetNewRandomTarget();
        }
        else
        {
            Vector2 direction = (randomTarget - (Vector2)transform.position).normalized;
            Vector2 newPosition = (Vector2)transform.position + (direction * speed * Time.deltaTime);
            rb.MovePosition(newPosition);
        }
    }

    //Establece una posici�n aleatoria en el mapa
    private void SetNewRandomTarget()
    {
        // Aqu� defines las coordenadas m�nimas y m�ximas de tu mapa generado aleatoriamente
        float randomX = Random.Range(0f, 16f);
        float randomY = Random.Range(0f, 8f);
        randomTarget = new Vector2(randomX, randomY);
    }
}
