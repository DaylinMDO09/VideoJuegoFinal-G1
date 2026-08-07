using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ComplexEnemy : MonoBehaviour
{
    [Header("Jugador")]
    public Transform player;

    [Header("Persecucion")]
    public float detectionRange = 5f;
    public float speed = 3f;

    [Header("Golpe")]
    public float tiempoHit = 0.35f;

    private Rigidbody2D rb2d;
    private Animator animator;

    private bool facingRight = true;
    private bool recibiendoHit = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (player == null)
        {
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");

            if (jugador != null)
            {
                player = jugador.transform;
            }
            else
            {
                Debug.LogError(
                    "No se encontro al jugador con el Tag Player"
                );
            }
        }
    }

    void FixedUpdate()
    {
        if (player == null)
            return;

        if (recibiendoHit)
        {
            rb2d.linearVelocity = new Vector2(
                0f,
                rb2d.linearVelocity.y
            );

            return;
        }

        float distancia = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distancia <= detectionRange)
        {
            float direccion;

            if (player.position.x > transform.position.x)
            {
                direccion = 1f;
            }
            else
            {
                direccion = -1f;
            }

            rb2d.linearVelocity = new Vector2(
                direccion * speed,
                rb2d.linearVelocity.y
            );

            if (direccion > 0 && !facingRight)
            {
                Flip();
            }
            else if (direccion < 0 && facingRight)
            {
                Flip();
            }

            if (animator != null)
            {
                animator.SetBool("isWalking", true);
            }
        }
        else
        {
            rb2d.linearVelocity = new Vector2(
                0f,
                rb2d.linearVelocity.y
            );

            if (animator != null)
            {
                animator.SetBool("isWalking", false);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void ReproducirHit()
    {
        if (!recibiendoHit)
        {
            StartCoroutine(AnimacionHit());
        }
    }

    private IEnumerator AnimacionHit()
    {
        recibiendoHit = true;

        rb2d.linearVelocity = new Vector2(
            0f,
            rb2d.linearVelocity.y
        );

        if (animator != null)
        {
            animator.SetBool("isWalking", false);
            animator.SetTrigger("Hit");
        }

        yield return new WaitForSeconds(tiempoHit);

        recibiendoHit = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            transform.position,
            detectionRange
        );
    }
}