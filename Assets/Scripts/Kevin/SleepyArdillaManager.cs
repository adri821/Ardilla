using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepyArdillaManager : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float tiempoEntreCambios = 5f;
    [SerializeField] private int minArdillasDormir = 1;
    [SerializeField] private int maxArdillasDormir = 3;

    private GameObject[] todasLasArdillas;

    void Start() {
        todasLasArdillas = GameObject.FindGameObjectsWithTag("ardilla");
        StartCoroutine(CicloTrabajoDescanso());
    }

    IEnumerator CicloTrabajoDescanso() {
        yield return new WaitForSeconds(tiempoEntreCambios);

        while (true) {
            // Dormir algunas ardillas aleatorias
            DesactivarArdillasAleatorias();
            yield return new WaitForSeconds(tiempoEntreCambios);
        }
    }

    void DesactivarArdillasAleatorias() {
        List<Move> ardillasActivas = new List<Move>();
        foreach (GameObject obj in todasLasArdillas) {
            Move comp = obj.GetComponent<Move>();
            if (comp != null && comp.trabajando) {
                ardillasActivas.Add(comp);
            }
        }

        if (ardillasActivas.Count == 0) return;

        int cantidad = Mathf.Min(Random.Range(minArdillasDormir, maxArdillasDormir + 1), ardillasActivas.Count);

        for (int i = 0; i < cantidad; i++) {
            int index = Random.Range(0, ardillasActivas.Count);
            ardillasActivas[index].SetTrabajando(false);
        }
    }
}
