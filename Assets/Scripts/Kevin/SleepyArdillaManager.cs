using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepyArdillaManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DesactivarArdillasCada5Segundos());
    }

    IEnumerator DesactivarArdillasCada5Segundos()
    {
        new WaitForSeconds(5f);
        while (true)
        {
            yield return new WaitForSeconds(5f);
            DesactivarArdillasAleatorias();
        }
    }

    void DesactivarArdillasAleatorias()
    {
        // 1. Obtener todas las ardillas activas (trabajando == true)
        GameObject[] todasLasArdillas = GameObject.FindGameObjectsWithTag("ardilla");
        List<ArdillaGolpe> ardillasActivas = new List<ArdillaGolpe>();

        foreach (GameObject obj in todasLasArdillas)
        {
            ArdillaGolpe comp = obj.GetComponent<ArdillaGolpe>();
            if (comp != null && comp.trabajando)
            {
                ardillasActivas.Add(comp);
            }
        }

        // Si no hay ardillas activas, salir
        if (ardillasActivas.Count == 0)
        {
            Debug.Log("No hay ardillas trabajando.");
            return;
        }

        // 2. Elegir un número aleatorio entre 1 y 3
        int cantidadADejarDeTrabajar = Random.Range(1, 4); // 1 a 3 (exclusivo superior)

        // 4. Si hay menos ardillas activas que el número aleatorio, solo cambia 1
        if (cantidadADejarDeTrabajar > ardillasActivas.Count)
        {
            cantidadADejarDeTrabajar = 1;
        }

        // 3. Elegir al azar "cantidadADejarDeTrabajar" ardillas y ponerlas en false
        for (int i = 0; i < cantidadADejarDeTrabajar; i++)
        {
            int index = Random.Range(0, ardillasActivas.Count);
            ardillasActivas[index].SetTrabajando(false);
            ardillasActivas.RemoveAt(index); // Evitar elegir la misma más de una vez
        }
        Debug.Log($"Se ha(n) dormido {cantidadADejarDeTrabajar} ardilla(s).");
    }
}
