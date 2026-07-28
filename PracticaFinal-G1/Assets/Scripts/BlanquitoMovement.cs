using UnityEngine;
using UnityEngine.InputSystem;
public class BlanquitoMovement : MonoBehaviour
{
    public float mover = 5f;
    public float salto = 8f;

    private Rigidbody2D rb2d;
    private float movimientoX;
    private bool saltando;
    private bool ensuelo;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            movimientoX = mover;
        }
        
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            movimientoX = -mover;
        }

        if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            saltando = true;
        }
    }
}
