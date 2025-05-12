using UnityEngine;

public class ArdillaGolpe : MonoBehaviour
{
    public enum EstadoArdilla { Trabajando, Durmiendo, Enfadada }

    public EstadoArdilla estadoActual;
    public Vector3 initialPosition;
    public Vector3 hiddenPosition;
    private Vector3 targetPosition;

    [Header("Configuración de Movimiento")]
    [SerializeField] private float hideDistance = 0.3f;
    [SerializeField] private float movementSpeed = 5f;

    [Header("Configuración de Estados")]
    [SerializeField] private float tiempoEnfado = 8f;
    [SerializeField] private float probabilidadDormir = 0.3f;
    [SerializeField] private ParticleSystem particulasSueno;
    [SerializeField] private ParticleSystem particulasEnfado;

    private bool canBeHit = true;
    private bool isMoving = false;
    private float tiempoEstadoActual = 0f;

    public delegate void OnEstadoChange(EstadoArdilla nuevoEstado, bool estaTrabajando);
    public static event OnEstadoChange EstadoCambiado;

    void Start() {
        initialPosition = transform.position;
        hiddenPosition = initialPosition + Vector3.down * hideDistance;
        targetPosition = hiddenPosition;
        estadoActual = EstadoArdilla.Trabajando;
        CambiarEstado(EstadoArdilla.Trabajando, true);
    }

    void Update() {
        if (isMoving) {
            // Mover suavemente hacia la posición objetivo
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementSpeed);

            // Comprobar si hemos llegado lo suficientemente cerca
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f) {
                transform.position = targetPosition;
                isMoving = false;
            }
        }

        tiempoEstadoActual += Time.deltaTime;
        if (estadoActual == EstadoArdilla.Durmiendo && tiempoEstadoActual > 8f) {
            // Probabilidad de despertar trabajando o seguir durmiendo
                CambiarEstado(EstadoArdilla.Trabajando);
            
        }
    }

    public void CambiarEstado(EstadoArdilla nuevoEstado, bool immediate = false) {
        if (estadoActual == nuevoEstado) return;

        estadoActual = nuevoEstado;
        tiempoEstadoActual = 0f;

        bool estaTrabajando = (estadoActual == EstadoArdilla.Trabajando);
        // Manejar partículas
        ManejarParticulas();

        // Actualizar posición y trabajo
        switch (estadoActual) {
            case EstadoArdilla.Trabajando:                
                EstadoCambiado?.Invoke(nuevoEstado, estaTrabajando);
                break;

            case EstadoArdilla.Durmiendo:
            case EstadoArdilla.Enfadada:
                targetPosition = initialPosition;
                EstadoCambiado?.Invoke(nuevoEstado, estaTrabajando);
                break;
        }

        if (immediate) {
            transform.position = targetPosition;
            isMoving = false;
        }
        else {
            isMoving = true;
        }
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
                Invoke("CalmarArdilla", tiempoEnfado);
                break;
        }
    }

    private void CalmarArdilla() {
        if (estadoActual == EstadoArdilla.Enfadada) {
            CambiarEstado(EstadoArdilla.Trabajando);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!canBeHit || isMoving) return;

        if ((other.CompareTag("LeftHand")) || (other.CompareTag("RightHand"))) {
            switch (estadoActual) {
                case EstadoArdilla.Durmiendo:
                    // Despertar trabajando
                    targetPosition = hiddenPosition;
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
