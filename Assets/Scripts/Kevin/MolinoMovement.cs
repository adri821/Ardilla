using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        SceneManager.sceneLoaded += OnSceneLoaded;

        InicializarReferencias();

        StartCoroutine("calcularGofio");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        // Reiniciar referencias cuando se carga una nueva escena
        InicializarReferencias();
    }

    void InicializarReferencias() {
        ArdillaGolpe.juegoTerminado = false;
        // Limpiar suscripción previa
        ArdillaGolpe.EstadoCambiado -= ActualizarTrabajoArdilla;

        // Obtener nuevas referencias
        ardillas = GameObject.FindGameObjectsWithTag("ardilla");
        ArdillaGolpe.EstadoCambiado += ActualizarTrabajoArdilla;

        // Actualizar contador inicial
        ardillasTrabajando = GetArdillas();
    }

    void OnDestroy() {
        // Limpiar suscripciones al destruir el objeto
        SceneManager.sceneLoaded -= OnSceneLoaded;
        ArdillaGolpe.EstadoCambiado -= ActualizarTrabajoArdilla;
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
        return cantidad;
    }

    private IEnumerator calcularGofio()
    {
        while (true)
        {
            yield return new WaitForSeconds(1F);
            CantidadGofio += ardillasTrabajando * 0.15f;
            gofioUI.text = ((int)CantidadGofio).ToString();
        }
    }

    private void calcularPuntuacion()
    {
        foreach (GameObject obj in ardillas) {
            ArdillaGolpe comp = obj.GetComponent<ArdillaGolpe>();
            if (comp != null) {
                if (comp.particulasSueno != null) comp.particulasSueno.Stop();
                if (comp.particulasEnfado != null) comp.particulasEnfado.Stop();
                comp.isMoving = true;
                comp.transform.position = Vector3.Lerp(comp.transform.position, comp.initialPosition, Time.deltaTime * comp.movementSpeed);
                comp.isMoving = false;
                comp.StopAllCoroutines();
                
            }
        }
        ArdillaGolpe.juegoTerminado = true;
        hands.GetComponent<ActiveHands>().SetInteractorsActive(true);
        puntuacion = CantidadGofio * 1000;
        panel.SetActive(true);
        puntuacionCalculada = true;
        gofioTxt.text = $"Kilos de gofio: {(int)CantidadGofio}";
        puntuacionTxt.text = ((int)puntuacion).ToString();

    }
}