using UnityEngine;

public class MenuTrigger : MonoBehaviour
{
    public GameObject sccontroller;

    private void OnTriggerEnter(Collider other) {
        if ((other.CompareTag("LeftHand")) || (other.CompareTag("RightHand"))) {
            sccontroller.GetComponent<SCController>().ChangeScene("Menu");
        }
    }
}
