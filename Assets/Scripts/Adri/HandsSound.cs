using UnityEngine;

public class HandsSound : MonoBehaviour {

    public SonidoArdilla sonido;

    private void OnTriggerEnter(Collider other) {
        sonido.PlaySFX("GolpeMartillo");
    }
}
