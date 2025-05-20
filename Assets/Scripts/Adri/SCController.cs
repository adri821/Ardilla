using UnityEngine;

public class SCController : MonoBehaviour
{
    public void ChangeScene(string scene) {
        SCManager.instance.LoadScene(scene);
    }

    public void ReloadScene(string scene) {
        ArdillaGolpe.EstadoCambiado -= null;
        SCManager.instance.LoadScene(scene);
    }

    public void ExitGame() {
        SCManager.instance.ExitGame();
    }
}
