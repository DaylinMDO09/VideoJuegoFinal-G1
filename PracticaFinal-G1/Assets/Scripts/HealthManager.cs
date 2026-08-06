using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image[] corazones;

    public Sprite corazonLleno;
    public Sprite corazonVacio;

    public int vidaMaxima = 5;
    public int vidaActual;

    public GameObject pantallaDerrota;
    public float tiempoParaReiniciar = 2f;

    private bool jugadorMuerto;

    void Start()
    {
        vidaActual = vidaMaxima;
        jugadorMuerto = false;

        if (pantallaDerrota != null)
        {
            pantallaDerrota.SetActive(false);
        }

        ActualizarCorazones();
    }

    public void RecibirDanio(int cantidad)
    {
        if (jugadorMuerto)
            return;

        vidaActual -= cantidad;

        if (vidaActual < 0)
        {
            vidaActual = 0;
        }

        ActualizarCorazones();

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    public void PerderTodasLasVidas()
    {
        if (jugadorMuerto)
            return;

        vidaActual = 0;
        ActualizarCorazones();
        Morir();
    }

    public void Curar(int cantidad)
    {
        if (jugadorMuerto)
            return;

        vidaActual += cantidad;

        if (vidaActual > vidaMaxima)
        {
            vidaActual = vidaMaxima;
        }

        ActualizarCorazones();
    }

    void ActualizarCorazones()
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            if (corazones[i] == null)
                continue;

            if (i < vidaActual)
            {
                corazones[i].sprite = corazonLleno;
            }
            else
            {
                corazones[i].sprite = corazonVacio;
            }
        }
    }

    void Morir()
    {
        jugadorMuerto = true;

        if (pantallaDerrota != null)
        {
            pantallaDerrota.SetActive(true);
        }

        StartCoroutine(ReiniciarEscena());
    }

    IEnumerator ReiniciarEscena()
    {
        yield return new WaitForSeconds(tiempoParaReiniciar);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}