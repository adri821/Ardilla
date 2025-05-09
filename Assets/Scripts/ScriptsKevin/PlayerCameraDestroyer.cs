using UnityEngine;
using Photon.Pun;
public class PlayerCameraDestroyer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!GetComponent<PhotonView>().IsMine) {
            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                Destroy(mainCam.gameObject);
            }
        }
    }
}
