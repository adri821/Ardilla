using System.Collections;
using UnityEngine;

public class ColliderLogic : MonoBehaviour
{
    public bool Contacto = false;

    public void OnTriggerEnter(Collider other)
    {
        StartCoroutine(MartilloContacto());
    }

    public IEnumerator MartilloContacto()
    {
        Contacto = true;
        yield return new WaitForSeconds(1.5f);
        Contacto = false;
        StopCoroutine(MartilloContacto());
    }
}
