using UnityEngine;

public class MolinoMovement : MonoBehaviour
{
    public float rotationSpeed;
    private int ardillasTrabajando;

    void Start()
    {
        ArdillaTrabajando.TrabajandoChange += ContarArdillas;
        ContarArdillas(true);
    }

    void Update()
    {
        if (ardillasTrabajando > 0)
        {
            transform.Rotate(Vector3.forward * (rotationSpeed * ardillasTrabajando) * Time.deltaTime, Space.World);
        }
    }

    private void ContarArdillas(bool trabajando)
    {
        ardillasTrabajando = GetArdillas();
    }

    public int GetArdillas()
    {
        GameObject[] ardillas = GameObject.FindGameObjectsWithTag("ardilla");
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
        Debug.Log("Math: " + (rotationSpeed * cantidad));
        return cantidad;
    }
}