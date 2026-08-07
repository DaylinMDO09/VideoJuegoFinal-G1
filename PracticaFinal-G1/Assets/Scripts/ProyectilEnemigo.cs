using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProyectilEnemigo : MonoBehaviour
{
    [Header("Proyectil")]
    public float velocidad = 12f;
    public float tiempoDeVida = 4f;
    public int danio = 1;

    private Rigidbody2D rb2d;
    private Vector2 direccion;
    private bool impacto = false;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }

    public void EstablecerDireccion(Vector2 nuevaDireccion)
    {
        direccion = nuevaDireccion.normalized;

        rb2d.linearVelocity = direccion * velocidad;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (impacto)
            return;

        if (collision.CompareTag("Player"))
        {
            impacto = true;

            HealthManager healthManager =
                FindAnyObjectByType<HealthManager>();

            if (healthManager != null)
            {
                healthManager.RecibirDanio(danio);
            }
            else
            {
                Debug.LogError(
                    "El objeto HealthManager existe, pero no se encontro el componente HealthManager."
                );
            }

            Destroy(gameObject);
        }
    }
}