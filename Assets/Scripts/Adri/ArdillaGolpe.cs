using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ArdillaGolpe : MonoBehaviour
{
    public bool trabajando;
    public Vector3 initialPosition;
    public Vector3 hiddenPosition;
    private Vector3 targetPosition;

    [Header("Configuración de Movimiento")]
    [SerializeField] private float hideDistance = 0.3f;
    [SerializeField] private float movementSpeed = 5f;

    private bool canBeHit = true;
    private bool isMoving = false;

    public delegate void OnTrabajandoChange(bool trabajando);
    public static event OnTrabajandoChange TrabajandoChange;

    void Start() {
        initialPosition = transform.position;
        hiddenPosition = initialPosition + Vector3.down * hideDistance;
        targetPosition = hiddenPosition;
        SetTrabajando(true, immediate: true);

        if (!GetComponent<Collider>()) {
            gameObject.AddComponent<BoxCollider>();
        }
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
    }

    public void ToggleTrabajando() {
        SetTrabajando(!trabajando);
    }

    public void SetTrabajando(bool estado, bool immediate = false) {
        if (trabajando != estado) {
            trabajando = estado;
            UpdatePosition(immediate);
            TrabajandoChange?.Invoke(trabajando);
        }
    }

    private void UpdatePosition(bool immediate) {
        targetPosition = trabajando ? hiddenPosition : initialPosition;

        if (immediate) {
            transform.position = targetPosition;
            isMoving = false;
        }
        else {
            isMoving = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!canBeHit || isMoving) return;

        if ((other.CompareTag("LeftHand")) || (other.CompareTag("RightHand")))
        {
            if (!trabajando) {
                SetTrabajando(true);
            }
        }
    }
}