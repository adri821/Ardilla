using UnityEngine;
using System.Collections;

public class ArdillaGolpeChris : MonoBehaviour
{
    public enum EstadoArdilla { Trabajando, Durmiendo, Enfadada }

    public static bool juegoTerminado = false;

    public EstadoArdilla estadoActual;
    public Vector3 initialPosition;
    public Vector3 hiddenPosition;
    private Vector3 targetPosition;
    private Vector3 nextPosition;

    [Header("Configuración de Movimiento")]
    [SerializeField] private float hideDistance = 0.3f;
    [SerializeField] public float movementSpeed = 2f;
    [SerializeField] private float tiempoEntreMovimientos = 2f;

    [Header("Configuración de Estados")]
    [SerializeField] private float tiempoEnfado = 8f;
    [SerializeField] public ParticleSystem particulasSueno;
    [SerializeField] public ParticleSystem particulasEnfado;

    private bool estaEnPosicionSuperior = false;

    private Coroutine movimientoCoroutine;
    public bool isMoving = false;

    private Animator animator;

    public delegate void OnEstadoChange(EstadoArdilla nuevoEstado, bool estaTrabajando);
    public static event OnEstadoChange EstadoCambiado;

    // VARIABLE PRUEBA
    [SerializeField] private float umbralAltura = 1.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Trabajando", true);
        animator.SetBool("Durmiendo", false);
        animator.SetBool("Enfadar", false);
        animator.SetBool("Despertar", false);

        initialPosition = transform.position;
        hiddenPosition = initialPosition + Vector3.down * hideDistance;

        estadoActual = EstadoArdilla.Trabajando;
        transform.position = hiddenPosition;
        targetPosition = initialPosition;
        nextPosition = hiddenPosition;
        StopAllCoroutines();

        movimientoCoroutine = StartCoroutine(CicloMovimiento());
        CambiarEstado(EstadoArdilla.Trabajando, true);
    }

    void Update()
    {
        if (juegoTerminado) return;

        if (isMoving)
        {
            MoverArdilla();
        }

        bool ahoraEnSuperior = Vector3.Distance(transform.position, initialPosition) < 0.1f;

        // Solo verificar cambios si el estado es Durmiendo
        if (estadoActual == EstadoArdilla.Durmiendo)
        {
            // Si cambió la posición respecto a la última comprobación
            if (estaEnPosicionSuperior != ahoraEnSuperior)
            {
                estaEnPosicionSuperior = ahoraEnSuperior;
                ActualizarParticulasSueno();
            }
        }
        else
        {
            estaEnPosicionSuperior = false;
        }
    }
    void OnEnable()
    {
        // Reiniciar estado al activarse (útil al recargar escena)
        CambiarEstado(EstadoArdilla.Trabajando, true);
    }

    void OnDestroy()
    {
        // Limpiar cualquier posible suscripción
        CancelInvoke();
    }


    private void MoverArdilla()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementSpeed);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            isMoving = false;

            // Solo cambiar estado si llegó al destino trabajando
            if (estadoActual != EstadoArdilla.Enfadada)
            {
                targetPosition = nextPosition;
                nextPosition = (targetPosition == hiddenPosition) ? initialPosition : hiddenPosition;
            }
        }
    }

    public IEnumerator CicloMovimiento()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            if (!isMoving && estadoActual != EstadoArdilla.Enfadada)
            {
                isMoving = true;
            }
            yield return new WaitForSeconds(tiempoEntreMovimientos);
        }
    }

    public void CambiarEstado(EstadoArdilla nuevoEstado, bool immediate = false)
    {
        if (estadoActual == nuevoEstado) return;

        animator.SetBool("Trabajando", false);
        animator.SetBool("Durmiendo", false);
        animator.SetBool("Enfadar", false);
        animator.SetBool("Despertar", false);

        // Manejar transiciones de animación
        switch (nuevoEstado)
        {
            case EstadoArdilla.Trabajando:
                if (estadoActual == EstadoArdilla.Durmiendo)
                {
                    // Secuencia: Durmiendo -> Stun -> Trabajando
                    animator.SetBool("Despertar", true);
                    StartCoroutine(TransitionToWorking());
                }
                else
                {
                    animator.SetBool("Trabajando", true);
                }
                break;

            case EstadoArdilla.Durmiendo:
                animator.SetBool("Durmiendo", true);
                break;

            case EstadoArdilla.Enfadada:
                animator.SetBool("Enfadar", true);
                break;
        }

        // Detener solo si se está enfadando
        if (nuevoEstado == EstadoArdilla.Enfadada && movimientoCoroutine != null)
        {
            StopCoroutine(movimientoCoroutine);
            movimientoCoroutine = null;
        }
        // Reiniciar movimiento si estaba enfadada y ahora no
        else if (estadoActual == EstadoArdilla.Enfadada && nuevoEstado != EstadoArdilla.Enfadada)
        {
            movimientoCoroutine = StartCoroutine(CicloMovimiento());
        }

        estadoActual = nuevoEstado;

        bool estaTrabajando = (estadoActual == EstadoArdilla.Trabajando);
        // Manejar partículas
        ManejarParticulas();

        // Actualizar posición y trabajo
        switch (estadoActual)
        {
            case EstadoArdilla.Trabajando:
                targetPosition = hiddenPosition;
                nextPosition = initialPosition;
                break;

            case EstadoArdilla.Durmiendo:
                if (!isMoving)
                {
                    targetPosition = (transform.position == initialPosition) ? hiddenPosition : initialPosition;
                    nextPosition = (targetPosition == hiddenPosition) ? initialPosition : hiddenPosition;
                    isMoving = true;
                }
                break;
            case EstadoArdilla.Enfadada:
                targetPosition = initialPosition;
                Invoke("CalmarArdilla", tiempoEnfado);
                break;
        }
        EstadoCambiado?.Invoke(nuevoEstado, estaTrabajando);
    }

    private IEnumerator TransitionToWorking()
    {
        // Espera a que termine la animación Stun (ajusta este tiempo según tu animación)
        yield return new WaitForSeconds(2.0f);

        animator.SetBool("Despertar", false);
        animator.SetBool("Trabajando", true);
    }

    private void ActualizarParticulasSueno()
    {
        if (particulasSueno == null) return;

        if (estaEnPosicionSuperior && estadoActual == EstadoArdilla.Durmiendo)
        {
            if (!particulasSueno.isPlaying)
            {
                particulasSueno.Play();
            }
        }
        else
        {
            if (particulasSueno.isPlaying)
            {
                particulasSueno.Stop();
            }
        }
    }

    private void ManejarParticulas()
    {
        // Detener todas las partículas primero
        if (particulasSueno != null) particulasSueno.Stop();
        if (particulasEnfado != null) particulasEnfado.Stop();

        // Activar las correspondientes
        switch (estadoActual)
        {
            case EstadoArdilla.Durmiendo:
                break;

            case EstadoArdilla.Enfadada:
                if (particulasEnfado != null) particulasEnfado.Play();
                break;
        }
    }

    private void CalmarArdilla()
    {
        if (estadoActual == EstadoArdilla.Enfadada)
        {
            CambiarEstado(EstadoArdilla.Trabajando);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isMoving) return;

        float diferenciaAltura = other.transform.position.y - transform.position.y;

        if ((other.CompareTag("LeftHand") && diferenciaAltura > umbralAltura) || (other.CompareTag("RightHand") && diferenciaAltura > umbralAltura))
        {
            switch (estadoActual)
            {
                case EstadoArdilla.Durmiendo:
                    // Despertar trabajando
                    isMoving = true;
                    CambiarEstado(EstadoArdilla.Trabajando);
                    break;

                case EstadoArdilla.Trabajando:
                    // Enfadar si está trabajando arriba
                    if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
                    {
                        CambiarEstado(EstadoArdilla.Enfadada);
                    }
                    break;

                case EstadoArdilla.Enfadada:
                    // No hacer nada si ya está enfadada
                    break;
            }
        }
    }
}
