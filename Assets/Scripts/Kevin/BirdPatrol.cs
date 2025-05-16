using UnityEngine;

public class BirdPatrol : MonoBehaviour
{
    public Transform[] puntosRuta;
    public float velocidad = 2f;
    private int indiceActual = 0;

    void Update()
    {
        if (puntosRuta.Length == 0) return;

        Transform objetivo = puntosRuta[indiceActual];

        //rotacion
        Vector3 direccion = -(objetivo.position - transform.position).normalized;
        if (direccion != Vector3.zero)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 5f);
        }
        //movimiento
        transform.position = Vector3.MoveTowards(transform.position, objetivo.position, velocidad * Time.deltaTime);

        if (Vector3.Distance(transform.position, objetivo.position) < 0.1f)
        {
            // Vuelve al primer punto al acabar
            indiceActual = (indiceActual + 1) % puntosRuta.Length;
        }
    }
}