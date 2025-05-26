using UnityEngine;

public class DificultadManager : MonoBehaviour
{
    public GameObject UIMenuOpciones, UIElegirDificultad;

    void ElegirDificultad()
    {
        UIMenuOpciones.SetActive(false);
        UIElegirDificultad.SetActive(true);
    }

    void VolverMenuOpciones()
    {
        UIElegirDificultad.SetActive(false);
        UIMenuOpciones.SetActive(true);
    }
}
