using UnityEngine;

public class GoatPatrol : MonoBehaviour
{
    public Transform[] puntosRuta;
    public float velocidad = 2f;

    private int indiceActual = 0;
    private Animator animator;
    private bool esperando = false;

    private enum EstadoCabra { Caminando, Idle1, Comiendo, Idle2 }
    private EstadoCabra estadoActual = EstadoCabra.Caminando;

    void Start()
    {
        animator = GetComponent<Animator>();
        IrAlPunto();
    }

    void Update()
    {
        if (puntosRuta.Length == 0 || esperando) return;

        if (estadoActual == EstadoCabra.Caminando)
        {
            Transform objetivo = puntosRuta[indiceActual];

            //Rota hacia el punto de patrulla
            Vector3 direccion = (objetivo.position - transform.position).normalized;
            if (direccion != Vector3.zero)
            {
                Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 5f);
            }

            //Se dirigue al punto de patrulla
            transform.position = Vector3.MoveTowards(transform.position, objetivo.position, velocidad * Time.deltaTime);

            if (Vector3.Distance(transform.position, objetivo.position) < 5f)
            {
                StartCoroutine(EsperarSecuencia());
            }
        }
    }

    void IrAlPunto()
    {
        animator.SetTrigger("Walking");
        estadoActual = EstadoCabra.Caminando;
    }

    System.Collections.IEnumerator EsperarSecuencia()
    {
        esperando = true;

        // 1. Idle1
        animator.SetTrigger("Iddle");
        estadoActual = EstadoCabra.Idle1;
        yield return new WaitForSeconds(GetAnimacionDuracion("Iddle"));

        // 2. Comer
        animator.SetTrigger("Eating");
        estadoActual = EstadoCabra.Comiendo;
        yield return new WaitForSeconds(GetAnimacionDuracion("Eating"));

        // 3. Idle2
        animator.SetTrigger("Iddle");
        estadoActual = EstadoCabra.Idle2;
        yield return new WaitForSeconds(GetAnimacionDuracion("Iddle"));

        // 4. Ir al siguiente punto
        indiceActual = (indiceActual + 1) % puntosRuta.Length;
        IrAlPunto();
        esperando = false;
    }

    float GetAnimacionDuracion(string nombreAnimacion)
    {
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        foreach (var clip in ac.animationClips)
        {
            if (clip.name == nombreAnimacion)
                return clip.length;
        }
        return 1f; // valor por defecto
    }
}