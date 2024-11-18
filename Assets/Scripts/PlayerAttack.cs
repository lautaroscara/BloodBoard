using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Collider2D attackCollider; // Usar Collider2D para un juego 2D
    public float attackCooldown = 0.3f; // Cooldown en segundos

    [SerializeField] private Transform swordTransform;

    private bool isAttacking = false;

    void Start()
    {
        // Verificar y desactivar el collider al inicio
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }
        else
        {
            Debug.LogError("Attack Collider no está asignado en el Inspector.");
        }

        // Verificar que la espada est� asignada
        if (swordTransform == null)
        {
            Debug.LogError("Sword Transform no está asignado en el Inspector.");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Apuntar al mouse
        AimSwordAtMouse();

        if (Input.GetMouseButtonDown(0)) // 0 es el bot�n izquierdo del mouse
        {
            if (!isAttacking)
            {
                StartCoroutine(PerformAttack());
            }
            else
            {
                Debug.Log("En cooldown. Espera un momento antes de atacar de nuevo.");
            }
        }
    }

    void AimSwordAtMouse()
    {
        if (swordTransform == null)
            return;

        // Obtener la posici�n del mouse en el mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Asegurar que el z sea 0 para 2D

        // Calcular la direcci�n hacia el mouse
        Vector3 direction = (mousePosition - swordTransform.position).normalized;

        // Calcular el �ngulo en grados
        float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;

        // Aplicar la rotaci�n solo en el eje Z
        swordTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Attack()
    {
        // Activa el collider del ataque
        if (attackCollider != null) // Verifica si attackCollider est� asignado
        {
            attackCollider.enabled = true;
            // Desactiva el collider despu�s de un breve per�odo
            Invoke("DeactivateAttackCollider", 0.2f); // Ajusta el tiempo seg�n sea necesario
        }
        else
        {
            Debug.LogError("AttackCollider no est� asignado en el Inspector.");
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;

        // Activar el collider de la espada
        if (attackCollider != null)
        {
            attackCollider.enabled = true;
        }

        // Esperar un breve período para permitir que el collider detecte colisiones
        yield return new WaitForSeconds(0.2f); // Ajusta según la animación

        // Desactivar el collider de la espada
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }

        // Esperar el cooldown antes de permitir otro ataque
        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isAttacking && other.CompareTag("Enemy"))
        {
            Debug.Log("Ataque exitoso a un enemigo!");

            // Obtener el componente EnemyHealth y aplicar da�o
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(PlayerData.instance.hit);
            }
            else
            {
                Debug.LogWarning("El enemigo no tiene el componente EnemyHealth.");
            }
        }
    }
}
