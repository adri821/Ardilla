using UnityEngine;


//esto va a asignado o añadido a las ardillas,
//estas deben tener el tag de "ardilla para funcionar con el molino"
public class ArdillaTrabajando : MonoBehaviour
{
    public bool trabajando;

    public delegate void OnTrabajandoChange(bool trabajando);
    public static event OnTrabajandoChange TrabajandoChange;

    void Start()
    {
        trabajando = true; 
    }


    //cambia la condicion de trabajando o no
    public void SetTrabajando(bool estado)
    {
        if (trabajando != estado)
        {
            trabajando = estado;
            TrabajandoChange?.Invoke(trabajando); 
        }
    }
}