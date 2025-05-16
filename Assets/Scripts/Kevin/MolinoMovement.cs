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
    public Text puntuacionTxt, gofioTxt;
    void Start()
    {
        puntuacionCalculada = false;
        timeLeft = 10;
        CantidadGofio = 0;
        ardillas = GameObject.FindGameObjectsWithTag("ardilla");
        ArdillaTrabajando.TrabajandoChange += ContarArdillas;
        ContarArdillas(true);
        StartCoroutine("calcularGofio");
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (ardillasTrabajando > 0)
            {
                transform.Rotate(Vector3.forward * (rotationSpeed * ardillasTrabajando) * Time.deltaTime, Space.World);
            }
        }
        else if (!puntuacionCalculada)
        {
            StopCoroutine("calcularGofio");
            calcularPuntuacion();
        }
    }

    private void ContarArdillas(bool trabajando)
    {
        ardillasTrabajando = GetArdillas();
    }

    public int GetArdillas()
    {
        
        int cantidad = 0;

        foreach (GameObject obj in ardillas)
        {
            ArdillaTrabajando comp = obj.GetComponent<ArdillaTrabajando>();
            if (comp != null && comp.trabajando)
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
            Debug.Log($"{CantidadGofio} Kilos de gofio tengo");
        }
    }

    private void calcularPuntuacion()
    {
        puntuacion = CantidadGofio * 1000;
        panel.SetActive(true);
        Debug.Log(puntuacion);
        puntuacionCalculada = true;
        gofioTxt.text = CantidadGofio.ToString();
        puntuacionTxt.text = puntuacion.ToString();

    }
}