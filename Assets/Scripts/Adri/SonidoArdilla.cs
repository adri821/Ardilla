using System.Collections.Generic;
using UnityEngine;

public class SonidoArdilla : MonoBehaviour
{
    public AudioSource sfxSource; // Componente AudioSource para efectos de sonido
    public AudioSource sfxHand;

    public Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();

    private void Start() {
        LoadSFXClips();
    }

    private void LoadSFXClips() {
        // Los recursos (ASSETS) que se cargan en TIEMPO DE EJECUCIÓN DEBEN ESTAR DENTRO de una carpeta denominada /Assets/Resources/SFX
        sfxClips["ArdillaEnfadada"] = Resources.Load<AudioClip>("SFX/ArdillaEnfadada");
        sfxClips["GolpeArdillaDormida"] = Resources.Load<AudioClip>("SFX/GolpeArdillaDormida");
        sfxClips["GolpeMartillo"] = Resources.Load<AudioClip>("SFX/GolpeMartillo");
    }

    //Para ardilla
    public void PlaySFX(string clipName) {
        if (sfxClips.ContainsKey(clipName)) {
            sfxSource.clip = sfxClips[clipName];
            sfxSource.Play();
        }
        else Debug.LogWarning("El AudioClip " + clipName + " no se encontró en el diccionario de sfxClips.");
    }

    //Para Manos
    public void PlayHands(string clipName) {
        if (sfxClips.ContainsKey(clipName)) {
            sfxHand.clip = sfxClips[clipName];
            sfxHand.Play();
        }
        else Debug.LogWarning("El AudioClip " + clipName + " no se encontró en el diccionario de sfxClips.");
    }
}
