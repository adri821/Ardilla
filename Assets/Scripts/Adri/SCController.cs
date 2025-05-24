using UnityEngine;

public class SCController : MonoBehaviour
{
    public void ChangeScene(string scene) {
        if (scene.Equals("Game")) {
            AudioManager.instance.PlayMusic("90");
        }
        if (scene.Equals("Menu")) {
            AudioManager.instance.PlayMusic("menu");
        }
        Fade.LoadScene(scene).SetFadeTime(1f);
    }

    public void ReloadScene(string scene) {
        AudioManager.instance.PlayMusic("90");
        ArdillaGolpe.EstadoCambiado -= null;
        Fade.LoadScene(scene).SetFadeTime(1f);
    }

    public void ExitGame() {
        SCManager.instance.ExitGame();
    }
}
