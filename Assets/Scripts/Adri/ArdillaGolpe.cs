using System.Collections;
using UnityEngine;

public class ArdillaGolpe : MonoBehaviour
{
    public enum EstadoArdilla { Trabajando, Durmiendo, Enfadada }

    public EstadoArdilla estadoActual;
    public Vector3 initialPosition;
    public Vector3 hiddenPosition;
    private Vector3 targetPosition;
    private Vector3 nextPosition;

    [Header("Configuración de Movimiento")]
    [SerializeField] private float hideDistance = 0.3f;
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float tiempoEntreMovimientos = 2f;

    [Header("Configuración de Estados")]
    [SerializeField] private float tiempoEnfado = 8f;
    [SerializeField] private ParticleSystem particulasSueno;
    [SerializeField] private ParticleSystem particulasEnfado;

    private Coroutine movimientoCoroutine;
    private bool isMoving = false;

    public delegate void OnEstadoChange(EstadoArdilla nuevoEstado, bool estaTrabajando);
    public static event OnEstadoChange EstadoCambiado;

    void Start() {
        initialPosition = transform.position;
        hiddenPosition = initialPosition + Vector3.down * hideDistance;

        estadoActual = EstadoArdilla.Trabajando;
        transform.position = hiddenPosition;
        targetPosition = initialPosition;
        nextPosition = hiddenPosition;

        movimientoCoroutine = StartCoroutine(CicloMovimiento());
        CambiarEstado(EstadoArdilla.Trabajando, true);        
    }

    void Update() {
        if (isMoving) {
            MoverArdilla();
        }
    }

    private void MoverArdilla() {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementSpeed);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f) {
            transform.position = targetPosition;
            isMoving = false;

            // Solo cambiar estado si llegó al destino trabajando
            if (estadoActual != EstadoArdilla.Enfadada) {
                targetPosition = nextPosition;
                nextPosition = (targetPosition == hiddenPosition) ? initialPosition : hiddenPosition;
            }
        }
    }

    public IEnumerator CicloMovimiento() {
        yield return new WaitForSeconds(1f);
        while (true) {
            if (!isMoving && estadoActual != EstadoArdilla.Enfadada) {
                isMoving = true;                
            }
            yield return new WaitForSeconds(tiempoEntreMovimientos);
        }
    }

    public void CambiarEstado(EstadoArdilla nuevoEstado, bool immediate = false) {
        if (estadoActual == nuevoEstado) return;

        // Detener solo si se está enfadando
        if (nuevoEstado == EstadoArdilla.Enfadada && movimientoCoroutine != null) {
            StopCoroutine(movimientoCoroutine);
            movimientoCoroutine = null;
        }
        // Reiniciar movimiento si estaba enfadada y ahora no
        else if (estadoActual == EstadoArdilla.Enfadada && nuevoEstado != EstadoArdilla.Enfadada) {
            movimientoCoroutine = StartCoroutine(CicloMovimiento());
        }

        estadoActual = nuevoEstado;

        bool estaTrabajando = (estadoActual == EstadoArdilla.Trabajando);
        // Manejar partículas
        ManejarParticulas();

        // Actualizar posición y trabajo
        switch (estadoActual) {
            case EstadoArdilla.Trabajando:
                targetPosition = hiddenPosition;
                nextPosition = initialPosition;                
                break;

            case EstadoArdilla.Durmiendo:
                if (!isMoving) {
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

    private void ManejarParticulas() {
        // Detener todas las partículas primero
        if (particulasSueno != null) particulasSueno.Stop();
        if (particulasEnfado != null) particulasEnfado.Stop();

        // Activar las correspondientes
        switch (estadoActual) {
            case EstadoArdilla.Durmiendo:
                if (particulasSueno != null) particulasSueno.Play();
                break;

            case EstadoArdilla.Enfadada:
                if (particulasEnfado != null) particulasEnfado.Play();
                break;
        }
    }

    private void CalmarArdilla() {
        if (estadoActual == EstadoArdilla.Enfadada) {
            CambiarEstado(EstadoArdilla.Trabajando);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (isMoving) return;

        if ((other.CompareTag("LeftHand")) || (other.CompareTag("RightHand"))) {
            switch (estadoActual) {
                case EstadoArdilla.Durmiendo:
                    // Despertar trabajando
                    isMoving = true;
                    CambiarEstado(EstadoArdilla.Trabajando);                    
                    break;

                case EstadoArdilla.Trabajando:
                    // Enfadar si está trabajando arriba
                    if (transform.position == initialPosition) {
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
