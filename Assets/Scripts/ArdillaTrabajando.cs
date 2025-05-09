using UnityEngine;

public class ArdillaTrabajando : MonoBehaviour
{
    public bool trabajando;

    public delegate void OnTrabajandoChange(bool trabajando);
    public static event OnTrabajandoChange TrabajandoChange;

    void Start()
    {
        trabajando = true; 
    }

    public void SetTrabajando(bool estado)
    {
        if (trabajando != estado)
        {
            trabajando = estado;
            TrabajandoChange?.Invoke(trabajando); 
        }
    }
}