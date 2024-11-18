using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Vector2 input;
    private Rigidbody2D rbd2;
    private Vector3 velocity;
    private float restartLevelDelay = 1f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rbd2 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        input = new Vector2(hor, ver).normalized;

        if (hor != 0 || ver != 0)
        {
            animator.SetFloat("Horizontal", hor);
            animator.SetFloat("Vertical", ver);
            animator.SetFloat("Speed", input.magnitude); // Puedes usar 1 directamente si no usas la magnitud para controlar la velocidad de animación.
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    void FixedUpdate()
    {
        rbd2.MovePosition(rbd2.position + input * PlayerData.instance.speed * Time.fixedDeltaTime);
        //rbd2.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Exit") 
        {
            Invoke ("NextLevel", restartLevelDelay*Time.deltaTime);
            enabled = false;
        }
    }

    private void NextLevel()
    {
        SceneManager.LoadScene (1);    
    }

}
