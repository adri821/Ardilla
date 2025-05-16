using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors.Visuals;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ActiveHands : MonoBehaviour
{
    [SerializeField] private GameObject leftController;
    [SerializeField] private GameObject rightController;

    public void SetInteractorsActive(bool active) {
        // Activar/desactivar los interactores de teleportación y selección
        if (leftController != null) {
            leftController.SetActive(active);
        }

        if (rightController != null) {
            rightController.SetActive(active);
        }
    }
}
