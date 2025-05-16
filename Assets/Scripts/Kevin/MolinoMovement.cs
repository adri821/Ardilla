using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MolinoMovement : MonoBehaviour
{
    public float rotationSpeed, CantidadGofio, timeLeft, puntuacion;
    private int ardillasTrabajando;
    GameObject[] ardillas;
    bool puntuacionCalculada;
    public GameObject panel;
    public Text puntuacionTxt, gofioTxt, gofioUI, timeUI;
    public GameObject hands;

    void Start()
    {
        ardillas = null;
        puntuacionCalculada = false;
        CantidadGofio = 0;
        StopAllCoroutines();
        ardillas = GameObject.FindGameObjectsWithTag("ardilla");
        
        StartCoroutine("calcularGofio");
        ArdillaGolpe.EstadoCambiado += ActualizarTrabajoArdilla;
        ardillasTrabajando = GetArdillas();
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timeUI.text = ((int)timeLeft).ToString();
            if (ardillasTrabajando > 0)
            {
                transform.Rotate(Vector3.forward * (rotationSpeed * ardillasTrabajando) * Time.deltaTime, Space.World);
            }
        }
        else if (!puntuacionCalculada)
        {
            timeUI.text = "0";
            StopCoroutine("calcularGofio");
            calcularPuntuacion();
        }
    }

    private void ActualizarTrabajoArdilla(ArdillaGolpe.EstadoArdilla nuevoEstado, bool estaTrabajando)
    {
        ardillasTrabajando = GetArdillas();
    }

    public int GetArdillas()
    {
        
        int cantidad = 0;
            foreach (GameObject obj in ardillas)
            {
                ArdillaGolpe comp = obj.GetComponent<ArdillaGolpe>();
                if (comp != null && comp.estadoActual == ArdillaGolpe.EstadoArdilla.Trabajando)
                {
                    cantidad++;
                }
            }
        
        Debug.Log("Ardillas trabajando: " + cantidad);
        Debug.Log("Velocidad molino: " + (rotationSpeed * cantidad));
        return cantidad;
    }

    private IEnumerator calcularGofio()
    {
        while (true)
        {
            yield return new WaitForSeconds(1F);
            CantidadGofio += ardillasTrabajando * 0.15f;
            gofioUI.text = ((int)CantidadGofio).ToString();
            Debug.Log($"{CantidadGofio} Kilos de gofio tengo");
        }
    }

    private void calcularPuntuacion()
    {
        hands.GetComponent<ActiveHands>().SetInteractorsActive(true);
        puntuacion = CantidadGofio * 1000;
        panel.SetActive(true);
        Debug.Log(puntuacion);
        puntuacionCalculada = true;
        gofioTxt.text = $"Kilos de gofio: {CantidadGofio.ToString()}";
        puntuacionTxt.text = puntuacion.ToString();

    }
}