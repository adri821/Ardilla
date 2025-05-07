using UnityEngine;

public class MolinoMovement : MonoBehaviour
{
    public float rotationSpeed;
    private int ardillasTrabajando;
    GameObject[] ardillas;
    void Start()
    {
        ardillas = GameObject.FindGameObjectsWithTag("ardilla");
        Move.TrabajandoChange += ContarArdillas;
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
        
        int cantidad = 0;

        foreach (GameObject obj in ardillas)
        {
            Move comp = obj.GetComponent<Move>();
            if (comp != null && comp.trabajando)
            {
                cantidad++;
            }
        }
        Debug.Log("Ardillas trabajando: " + cantidad);
        Debug.Log("Velocidad molino: " + (rotationSpeed * cantidad));
        return cantidad;
    }
}