using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticOnCollision : MonoBehaviour
{
    public ActionBasedController controller;
    [Range(0f, 1f)] public float amplitude = 0.9f;
    public float duration = 0.3f;

    private void OnTriggerEnter(Collider other)
    {
        if (controller != null)
        {
            controller.SendHapticImpulse(amplitude, duration);
        }
    }
}
