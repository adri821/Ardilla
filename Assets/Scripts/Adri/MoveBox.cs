using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class MoveBox : MonoBehaviour
{
    private XRPokeFollowAffordance pokeFollow;
    private Vector3 initialPosition;
    private Vector3 hiddenPosition;
    private bool isDown = false;

    [SerializeField] private float downDistance = 0.2f; // Cu�nto baja el topo
    [SerializeField] private float resetTime = 2.0f; // Tiempo para volver a subir

    private void Start() {
        pokeFollow = GetComponent<XRPokeFollowAffordance>();
        initialPosition = transform.position;
        hiddenPosition = initialPosition - Vector3.forward * downDistance;

        // Desactivar el seguimiento autom�tico al soltar
        pokeFollow.returnToInitialPosition = false;
    }

    public void OnPoke() {
        if (!isDown) {
            // Bajar el topo
            transform.position = hiddenPosition;
            isDown = true;

            // Programar el reseteo despu�s de un tiempo
            Invoke("ResetTopo", resetTime);
        }
    }

    private void ResetTopo() {
        // Volver a la posici�n inicial
        transform.position = initialPosition;
        isDown = false;
    }
}
