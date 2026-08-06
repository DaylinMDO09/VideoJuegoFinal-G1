using UnityEngine;
using UnityEngine.InputSystem;

public class BlanquitoMovement : MonoBehaviour
{
    public float mover = 5f;
    public float salto = 8f;

    [Header("Disparo")]
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 0.4f;

    private Rigidbody2D rb2d;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float movimientoX;
    private bool saltando;
    private bool ensuelo;
    private bool mirandoDerecha = true;
    private float siguienteDisparo;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator.enabled = true;
        animator.Play("Idle");
    }

    void Update()
    {
        movimientoX = 0f;

        if (Keyboard.current.dKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed)
        {
            movimientoX = mover;
            mirandoDerecha = true;
            spriteRenderer.flipX = false;
        }

        if (Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed)
        {
            movimientoX = -mover;
            mirandoDerecha = false;
            spriteRenderer.flipX = true;
        }

        if ((Keyboard.current.wKey.wasPressedThisFrame ||
             Keyboard.current.upArrowKey.wasPressedThisFrame) && ensuelo)
        {
            saltando = true;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame &&
            Time.time >= siguienteDisparo)
        {
            Disparar();
        }

        animator.SetFloat("Velocidad", Mathf.Abs(movimientoX));
        animator.SetBool("EnSuelo", ensuelo);
        animator.SetFloat("VelocidadY", rb2d.linearVelocity.y);
    }

    void FixedUpdate()
    {
        rb2d.linearVelocity = new Vector2(
            movimientoX,
            rb2d.linearVelocity.y
        );

        if (saltando && ensuelo)
        {
            rb2d.linearVelocity = new Vector2(
                rb2d.linearVelocity.x,
                salto
            );

            saltando = false;
            ensuelo = false;
        }
    }

    void Disparar()
    {
        if (proyectilPrefab == null)
        {
            Debug.LogError("No asignaste el Proyectil Prefab en Namor.");
            return;
        }

        if (puntoDisparo == null)
        {
            Debug.LogError("No asignaste el Punto Disparo en Namor.");
            return;
        }

        siguienteDisparo = Time.time + tiempoEntreDisparos;

        animator.SetTrigger("Golpeado");

        GameObject nuevoProyectil = Instantiate(
            proyectilPrefab,
            puntoDisparo.position,
            Quaternion.identity
        );

        Proyectil proyectil = nuevoProyectil.GetComponent<Proyectil>();

        if (proyectil == null)
        {
            Debug.LogError("El prefab no tiene el script Proyectil.");
            Destroy(nuevoProyectil);
            return;
        }

        if (mirandoDerecha)
        {
            proyectil.EstablecerDireccion(Vector2.right);
        }
        else
        {
            proyectil.EstablecerDireccion(Vector2.left);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ensuelo = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ensuelo = false;
    }
}