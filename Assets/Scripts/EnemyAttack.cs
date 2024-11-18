using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //Ataque
    private int damage = 1;
    private float attackCoolDown = 1.5f;
    private float attackRange = 1.0f; 
    private bool isAttacking = false;

    private Transform player;

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            if (!isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;

        // Aplicar daño al jugador
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        // Esperar el tiempo de ataque antes de poder atacar de nuevo
        yield return new WaitForSeconds(attackCoolDown);

        isAttacking = false;
    }

}
