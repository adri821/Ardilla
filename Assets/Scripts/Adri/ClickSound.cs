using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public void ClickButton() {
        AudioManager.instance.PlayMusic("click");
    }
}
