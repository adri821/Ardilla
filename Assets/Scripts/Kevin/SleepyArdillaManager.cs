using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepyArdillaManager : MonoBehaviour
{
    [SerializeField] private float tiempoEntreCambios = 5f;
    [SerializeField] private int minArdillasDormir = 1;
    [SerializeField] private int maxArdillasDormir = 3;

    private GameObject[] todasLasArdillas;

    void Start()
    {
        StopAllCoroutines();
        SceneManager.sceneLoaded += OnSceneLoaded;
        InicializarReferencias();
        StartCoroutine(CicloTrabajoDescanso());
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        InicializarReferencias();
    }

    void InicializarReferencias() {
        todasLasArdillas = GameObject.FindGameObjectsWithTag("ardilla");
    }

    void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    IEnumerator CicloTrabajoDescanso()
    {
        yield return new WaitForSeconds(tiempoEntreCambios);

        while (true) {
            // Solo intentar dormir ardillas que estén trabajando
            var ardillasTrabajando = todasLasArdillas
                .Select(a => a.GetComponent<ArdillaGolpe>())
                .Where(a => a.estadoActual == ArdillaGolpe.EstadoArdilla.Trabajando)
                .ToList();

            if (ardillasTrabajando.Count > 0) {
                int cantidad = Mathf.Min(Random.Range(minArdillasDormir, maxArdillasDormir + 1),
                                      ardillasTrabajando.Count);

                for (int i = 0; i < cantidad; i++) {
                    int index = Random.Range(0, ardillasTrabajando.Count);
                    ardillasTrabajando[index].CambiarEstado(ArdillaGolpe.EstadoArdilla.Durmiendo);
                    ardillasTrabajando.RemoveAt(index);
                }
            }

            yield return new WaitForSeconds(tiempoEntreCambios);
        }
    }
}
