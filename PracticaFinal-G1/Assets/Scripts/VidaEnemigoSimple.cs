using UnityEngine;

public class VidaEnemigoSimple : MonoBehaviour
{
    [SerializeField] private int vidaMaxima = 2;

    private int vidaActual;

    private void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;

        Debug.Log("Vida restante del enemigo: " + vidaActual);

        if (vidaActual <= 0)
        {
            Destroy(gameObject);
        }
    }
}