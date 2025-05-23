using Unity.XR.CoreUtils;
using UnityEngine;

public class StartPosition : MonoBehaviour
{
    public Vector3 startPosition;
    public XROrigin origin;

    void Start() {
        origin.MoveCameraToWorldLocation(startPosition);
    }
}
