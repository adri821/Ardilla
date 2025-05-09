using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class MoveDiagonal : MonoBehaviour
{
    private XRPokeFollowAffordance pokeFollow;
    private Vector3 initialPosition;
    private Vector3 hiddenPosition;
    private bool isDown = false;

    [SerializeField] private float downDistance = 0.2f; // Cuánto baja el topo
    [SerializeField] private float resetTime = 2.0f; // Tiempo para volver a subir
    [SerializeField] private Vector3 diagonalDirection = new Vector3(1, 0, 1);

    private void Start() {
        pokeFollow = GetComponent<XRPokeFollowAffordance>();
        initialPosition = transform.position;
        hiddenPosition = initialPosition - diagonalDirection.normalized * downDistance;

        // Desactivar el seguimiento automático al soltar
        pokeFollow.returnToInitialPosition = false;
    }

    public void OnPoke() {
        if (!isDown) {
            // Bajar el topo
            transform.position = hiddenPosition;
            isDown = true;

            // Programar el reseteo después de un tiempo
            Invoke("ResetTopo", resetTime);
        }
    }

    private void ResetTopo() {
        // Volver a la posición inicial
        transform.position = initialPosition;
        isDown = false;
    }
}
