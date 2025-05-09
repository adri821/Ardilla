using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class Move : MonoBehaviour
{
    public bool trabajando;
    private Vector3 initialPosition;
    private Vector3 hiddenPosition;
    private XRPokeFollowAffordance pokeFollow;

    public delegate void OnTrabajandoChange(bool trabajando);
    public static event OnTrabajandoChange TrabajandoChange;

    [Header("Configuración")]
    [SerializeField] private float hideDistance = 0.3f;

    void Start() {
        pokeFollow = GetComponent<XRPokeFollowAffordance>();
        initialPosition = transform.position;
        hiddenPosition = initialPosition + Vector3.down * hideDistance;

        pokeFollow.returnToInitialPosition = false;

        // Iniciar trabajando (abajo)
        SetTrabajando(true);
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

    // Método para interactuar con XR
    public void OnPoke() {
        ToggleTrabajando();
    }
}
