using System.Collections;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public int valor = 1;
    public CoinManager coinManager;

    private Animator animator;
    private Collider2D colision;
    private bool recogida;

    void Start()
    {
        animator = GetComponent<Animator>();
        colision = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || recogida)
            return;

        recogida = true;

        if (coinManager != null)
        {
            coinManager.AgregarMoneda(valor);
        }

        if (colision != null)
        {
            colision.enabled = false;
        }

        animator.SetTrigger("Recogida");

        StartCoroutine(DestruirDespuesDeAnimacion());
    }

    private IEnumerator DestruirDespuesDeAnimacion()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}