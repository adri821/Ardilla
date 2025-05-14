using System.Collections;
using UnityEngine;

<<<<<<< Updated upstream
public class WindPool : MonoBehaviour
=======
public class ParticleController : MonoBehaviour
>>>>>>> Stashed changes
{
    public float intervalo = 10f;        // Tiempo entre cada ciclo completo (debe ser mayor o igual a duracionActiva)
    public float duracionActiva = 9f;    // Tiempo que cada partícula se mantiene activa (Duration + StartLifetime)

    private void Start()
    {
        StartCoroutine(CicloDeParticulas());
    }

    IEnumerator CicloDeParticulas()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                int index = Random.Range(i * 3, (i + 1) * 3); // 0-2, 3-5, 6-8
                Transform hijo = transform.GetChild(index);
                ParticleSystem ps = hijo.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    ps.gameObject.SetActive(true);
                    ps.Play();
                    StartCoroutine(DesactivarTrasTiempo(ps, duracionActiva));
                }
            }

            yield return new WaitForSeconds(intervalo);
        }
    }

    IEnumerator DesactivarTrasTiempo(ParticleSystem ps, float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        ps.gameObject.SetActive(false);
    }
}
