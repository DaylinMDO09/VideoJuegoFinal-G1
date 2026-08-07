using UnityEngine;
using UnityEngine.Rendering;

public class MovimientoPlataforma : MonoBehaviour
{
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private float distanciaSuelo;
    [SerializeField] private bool movimientoDerecha = true;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D informacionSuelo = Physics2D.Raycast(
            controladorSuelo.position,
            Vector2.down,
            distanciaSuelo
        );

        rb2d.linearVelocity = new Vector2(
            velocidad,
            rb2d.linearVelocity.y
        );

        if (!informacionSuelo)
        {
            Girar();
        }
    }

    private void Girar()
    {
        movimientoDerecha = !movimientoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidad *= -1;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorSuelo.position, controladorSuelo.position + Vector3.down * distanciaSuelo);
    }
}
