using UnityEngine;

public class DificultadManager : MonoBehaviour
{
    public GameObject UIMenuOpciones, UIElegirDificultad;

    public void ElegirDificultad()
    {
        UIMenuOpciones.SetActive(false);
        UIElegirDificultad.SetActive(true);
    }

    public void VolverMenuOpciones()
    {
        UIElegirDificultad.SetActive(false);
        UIMenuOpciones.SetActive(true);
    }
}
