using Unity.XR.CoreUtils;
using UnityEngine;

public class StartPosition : MonoBehaviour
{
    public XROrigin xrOrigin;
    public Transform puntoInicioDeseado; 

    void Start()
    {
        Recentrar();
    }

    void Recentrar()
    {
        Transform cameraTransform = xrOrigin.Camera.transform;

        Vector3 offset = cameraTransform.position - xrOrigin.transform.position;
        Vector3 offsetXZ = new Vector3(offset.x, 0, offset.z);

        xrOrigin.transform.position = puntoInicioDeseado.position - offsetXZ;

        float anguloDeseadoY = puntoInicioDeseado.eulerAngles.y;
        float anguloCamaraY = cameraTransform.eulerAngles.y;
        float diferenciaRotacionY = anguloDeseadoY - anguloCamaraY;

        xrOrigin.transform.Rotate(0, diferenciaRotacionY, 0);
    }
}
