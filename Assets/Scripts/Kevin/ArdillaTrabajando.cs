using UnityEngine;

public class ArdillaTrabajando : MonoBehaviour
{
    public bool trabajando = true;

    public delegate void OnTrabajandoChange(bool trabajando);
    public static event OnTrabajandoChange TrabajandoChange;

    public void SetTrabajando(bool estado)
    {
        
         RPC_SetTrabajando(estado);
        
    }

    void RPC_SetTrabajando(bool estado)
    {
        if (trabajando != estado)
        {
            trabajando = estado;
            TrabajandoChange?.Invoke(trabajando);
        }
    }
}