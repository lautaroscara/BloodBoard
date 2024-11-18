using UnityEngine;

public class SwordAnim : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Asegúrate de que el objeto tenga un componente Animator
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No se encontró un Animator en el objeto de la espada.");
        }
    }

    void Update()
    {
        // Detecta el clic izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            TriggerAttackAnimation();
        }
    }

    private void TriggerAttackAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack"); // Activa la animación usando un Trigger
        }
    }
}