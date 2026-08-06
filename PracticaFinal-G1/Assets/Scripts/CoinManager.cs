using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public TMP_Text textoMonedas;

    private int monedasActuales = 0;

    void Start()
    {
        ActualizarTexto();
    }

    public void AgregarMoneda(int cantidad)
    {
        monedasActuales += cantidad;
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        if (textoMonedas != null)
        {
            textoMonedas.text = "x " + monedasActuales;
        }
    }
}