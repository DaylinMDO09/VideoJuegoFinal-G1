using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image[] corazones;

    public Sprite corazonLleno;
    public Sprite corazonVacio;

    public int vidaMaxima = 5;
    public int vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarCorazones();
    }

    public void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;

        if (vidaActual < 0)
            vidaActual = 0;

        ActualizarCorazones();

        if (vidaActual == 0)
        {
            Debug.Log("¡Has muerto!");
        }
    }

    public void Curar(int cantidad)
    {
        vidaActual += cantidad;

        if (vidaActual > vidaMaxima)
            vidaActual = vidaMaxima;

        ActualizarCorazones();
    }

    void ActualizarCorazones()
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < vidaActual)
                corazones[i].sprite = corazonLleno;
            else
                corazones[i].sprite = corazonVacio;
        }
    }
}