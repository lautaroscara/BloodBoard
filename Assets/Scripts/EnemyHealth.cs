using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private BoardManager boardManager;

    public float maxHealth = 10f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void SetBoardManager(BoardManager manager)
    {
        boardManager = manager;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        boardManager.enemysInstantiated--;
        if (boardManager != null)
        {
            boardManager.EnemyKilled();
        }
        
        Destroy(gameObject);
    }

}
