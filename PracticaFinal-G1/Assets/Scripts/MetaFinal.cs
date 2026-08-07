using UnityEngine;

public class MetaFinal : MonoBehaviour
{
    [SerializeField] private GameObject panelVictoria;

    private bool juegoGanado = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (juegoGanado)
            return;

        if (other.CompareTag("Player"))
        {
            juegoGanado = true;
            panelVictoria.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}