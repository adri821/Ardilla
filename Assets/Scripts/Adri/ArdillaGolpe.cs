using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ArdillaGolpe : MonoBehaviour
{
    public bool trabajando;
    public Vector3 initialPosition;
    public Vector3 hiddenPosition;

    public delegate void OnTrabajandoChange(bool trabajando);
    public static event OnTrabajandoChange TrabajandoChange;

    [Header("Configuración")]
    [SerializeField] private float hideDistance = 0.3f;
    [SerializeField] private float movementSpeed = 2f;

    private bool isMoving = false;


    void Start() {
        initialPosition = transform.position;
        hiddenPosition = initialPosition + Vector3.down * hideDistance;

        // Iniciar trabajando (abajo)
        SetTrabajando(true);
    }

    void Update() {
        if (isMoving) {
            // Mover suavemente hacia la posición objetivo
            transform.position = Vector3.Lerp(transform.position, hiddenPosition, Time.deltaTime * movementSpeed);

            // Comprobar si hemos llegado lo suficientemente cerca
            if (Vector3.Distance(transform.position, hiddenPosition) < 0.01f) {
                transform.position = hiddenPosition;
                isMoving = false;
            }
        }
    }

    public void ToggleTrabajando() {
        SetTrabajando(!trabajando);
    }

    public void SetTrabajando(bool estado) {
        if (trabajando != estado) {
            trabajando = estado;
            UpdatePosition();
            TrabajandoChange?.Invoke(trabajando);
        }
    }

    private void UpdatePosition() {
        if (trabajando) {
            // Mover a posición trabajando (abajo)
            transform.position = hiddenPosition;
        }
        else {
            // Mover a posición durmiendo (arriba)
            transform.position = initialPosition;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("LeftHand") || other.gameObject.CompareTag("RigthHand")) {
            if (!trabajando) {
                SetTrabajando(true);
            }
        }
    }
}
/*
 *     public bool trabajando;
    private Vector3 initialPosition;
    private Vector3 hiddenPosition;
    private Vector3 targetPosition;
    
    [Header("Configuración de Movimiento")]
    [SerializeField] private float hideDistance = 0.3f;
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float cooldownTime = 0.5f;
    
    private bool canBeHit = true;
    private bool isMoving = false;

    public delegate void OnTrabajandoChange(bool trabajando);
    public static event OnTrabajandoChange TrabajandoChange;

    void Start() 
    {
        initialPosition = transform.position;
        hiddenPosition = initialPosition + Vector3.down * hideDistance;
        targetPosition = hiddenPosition;
        SetTrabajando(true, immediate: true);
        
        if (!GetComponent<Collider>())
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }

    void Update()
    {
        if (isMoving)
        {
            // Mover suavemente hacia la posición objetivo
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementSpeed);
            
            // Comprobar si hemos llegado lo suficientemente cerca
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    public void ToggleTrabajando() 
    {
        SetTrabajando(!trabajando);
    }

    public void SetTrabajando(bool estado, bool immediate = false) 
    {
        if (trabajando != estado) 
        {
            trabajando = estado;
            UpdatePosition(immediate);
            TrabajandoChange?.Invoke(trabajando);
        }
    }

    private void UpdatePosition(bool immediate) 
    {
        targetPosition = trabajando ? hiddenPosition : initialPosition;
        
        if (immediate)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (!canBeHit || isMoving) return;
        
        if ((other.CompareTag("LeftHand") || other.CompareTag("RightHand")) 
        {
            if (!trabajando) 
            {
                SetTrabajando(true);
                StartCooldown();
            }
        }
    }

    private void StartCooldown()
    {
        canBeHit = false;
        Invoke(nameof(ResetCooldown), cooldownTime);
    }

    private void ResetCooldown()
    {
        canBeHit = true;
    }
}
 */