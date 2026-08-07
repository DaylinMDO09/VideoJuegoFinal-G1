using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FinalBoss : MonoBehaviour
{
    [Header("Jugador")]
    public Transform player;

    [Header("Movimiento")]
    public float detectionRange = 12f;
    public float speed = 5f;
    public float distanciaMinima = 3f;

    [Header("Disparo")]
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 1.2f;

    private Rigidbody2D rb2d;
    private Animator animator;

    private bool facingRight = true;
    private float siguienteDisparo;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (player == null)
        {
            GameObject jugador =
                GameObject.FindGameObjectWithTag("Player");

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

        float distancia = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distancia > detectionRange)
        {
            Detenerse();
            return;
        }

        float diferenciaX =
            player.position.x - transform.position.x;

        float direccion = Mathf.Sign(diferenciaX);

        if (direccion > 0 && !facingRight)
        {
            Flip();
        }
        else if (direccion < 0 && facingRight)
        {
            Flip();
        }

        if (Mathf.Abs(diferenciaX) > distanciaMinima)
        {
            rb2d.linearVelocity = new Vector2(
                direccion * speed,
                rb2d.linearVelocity.y
            );

            if (animator != null)
            {
                animator.SetBool("isWalking", true);
            }
        }
        else
        {
            Detenerse();
        }

        if (Time.time >= siguienteDisparo)
        {
            Disparar();
            siguienteDisparo =
                Time.time + tiempoEntreDisparos;
        }
    }

    void Detenerse()
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

    void Disparar()
    {
        if (proyectilPrefab == null || puntoDisparo == null)
            return;

        GameObject proyectil = Instantiate(
            proyectilPrefab,
            puntoDisparo.position,
            Quaternion.identity
        );

        Vector2 direccion =
            (player.position - puntoDisparo.position).normalized;

        ProyectilEnemigo scriptProyectil =
            proyectil.GetComponent<ProyectilEnemigo>();

        if (scriptProyectil != null)
        {
            scriptProyectil.EstablecerDireccion(direccion);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    public void ReproducirHit()
    {
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            detectionRange
        );
    }
}