using UnityEngine;

public class CaidaAlVacio : MonoBehaviour
{
    [SerializeField] private GameObject PantallaDerrota;
    public HealthManager healthManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            healthManager.PerderTodasLasVidas();
        }
    }
}