using UnityEngine;

public class Spike : MonoBehaviour
{
    public int danio = 1;
    public float fuerzaEmpujeHorizontal = 6f;
    public float fuerzaEmpujeVertical = 5f;

    public HealthManager healthManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (healthManager != null)
        {
            healthManager.RecibirDanio(danio);
        }

        Rigidbody2D rbJugador = collision.GetComponent<Rigidbody2D>();

        if (rbJugador != null)
        {
            float direccionEmpuje;

            if (collision.transform.position.x < transform.position.x)
            {
                direccionEmpuje = -1f;
            }
            else
            {
                direccionEmpuje = 1f;
            }

            rbJugador.linearVelocity = Vector2.zero;

            rbJugador.AddForce(
                new Vector2(
                    direccionEmpuje * fuerzaEmpujeHorizontal,
                    fuerzaEmpujeVertical
                ),
                ForceMode2D.Impulse
            );
        }
    }
}