using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float velocidad = 10f;
    public float tiempoDeVida = 3f;

    private Rigidbody2D rb2d;

    private Vector2 direccion;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.linearVelocity = direccion * velocidad;
        Destroy(gameObject, tiempoDeVida);
    }

    public void EstablecerDireccion(Vector2 nuevaDireccion)
    {
        direccion = nuevaDireccion.normalized;
    }

    void FixedUpdate()
    {
        transform.Translate(direccion * velocidad * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        VidaEnemigoSimple enemigo = collision.GetComponentInParent<VidaEnemigoSimple>();

        if (enemigo != null)
        {
            enemigo.RecibirDanio(1);
            Destroy(gameObject);
        }
    }
}