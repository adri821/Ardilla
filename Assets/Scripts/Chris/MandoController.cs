using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class MandoController : MonoBehaviour
{
    private InputDevice mandoIzquierdo;
    private InputDevice mandoDerecho;

    void Start()
    {
        // Obtener el dispositivo del mando izquierdo
        var dispositivosIzquierdos = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, dispositivosIzquierdos);
        if (dispositivosIzquierdos.Count > 0)
        {
            mandoIzquierdo = dispositivosIzquierdos[0];
        }

        // Obtener el dispositivo del mando derecho
        var dispositivosDerechos = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, dispositivosDerechos);
        if (dispositivosDerechos.Count > 0)
        {
            mandoDerecho = dispositivosDerechos[0];
        }

        List<InputDevice> dispositivos = new List<InputDevice>();
        InputDevices.GetDevices(dispositivos);

        foreach (var dispositivo in dispositivos)
        {
            Debug.Log($"Dispositivo detectado: {dispositivo.name}, Características: {dispositivo.characteristics}");
        }
    }

    void Update()
    {
        // Verificar movimiento vertical del mando izquierdo
        if (mandoIzquierdo.isValid)
        {
            Vector3 velocidadIzquierda;
            if (mandoIzquierdo.TryGetFeatureValue(CommonUsages.deviceVelocity, out velocidadIzquierda))
            {
                if (velocidadIzquierda.y < -0.1f)
                {
                    Debug.Log("El mando izquierdo se mueve hacia abajo");
                }
                else if (velocidadIzquierda.y > 0.1f)
                {
                    Debug.Log("El mando izquierdo se mueve hacia arriba");
                }
            }
        }

        // Verificar movimiento vertical del mando derecho
        if (mandoDerecho.isValid)
        {
            Vector3 velocidadDerecha;
            if (mandoDerecho.TryGetFeatureValue(CommonUsages.deviceVelocity, out velocidadDerecha))
            {
                if (velocidadDerecha.y < -0.1f)
                {
                    Debug.Log("El mando derecho se mueve hacia abajo");
                }
                else if (velocidadDerecha.y > 0.1f)
                {
                    Debug.Log("El mando derecho se mueve hacia arriba");
                }
            }
        }
    }
 }

