using UnityEngine;
using UnityEngine.InputSystem;

public class BlanquitoMovement : MonoBehaviour
{
    public float mover = 5f;
    public float salto = 8f;

    private Rigidbody2D rb2d;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float movimientoX;
    private bool saltando;
    private bool ensuelo;

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
            spriteRenderer.flipX = false;
        }

        if (Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed)
        {
            movimientoX = -mover;
            spriteRenderer.flipX = true;
        }

        if ((Keyboard.current.wKey.wasPressedThisFrame ||
             Keyboard.current.upArrowKey.wasPressedThisFrame) && ensuelo)
        {
            saltando = true;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ensuelo = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ensuelo = false;
    }
}