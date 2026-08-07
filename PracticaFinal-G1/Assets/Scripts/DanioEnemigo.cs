using UnityEngine;

public class DanioEnemigo : MonoBehaviour
{
    [SerializeField] private int danio = 1;
    [SerializeField] private float fuerzaEmpuje = 8f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BlanquitoMovement jugador =
            collision.collider.GetComponentInParent<BlanquitoMovement>();

        if (jugador != null)
        {
            HealthManager healthManager =
                FindAnyObjectByType<HealthManager>();

            if (healthManager != null)
            {
                healthManager.RecibirDanio(danio);
            }
            else
            {
                Debug.LogError("No se encontró HealthManager en la escena.");
            }

            jugador.RecibirGolpe(transform.position, fuerzaEmpuje);
        }
    }
}