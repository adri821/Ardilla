using UnityEngine;

public class TransicionScript : MonoBehaviour
{

    [SerializeField] private Transform destino;
    [SerializeField] private Transform Inicio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = Vector3.MoveTowards(Inicio.position, destino.position, 2 * Time.deltaTime);
    }


}
