using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public void ClickButton() {
        AudioManager.instance.PlaySFX("Click");
    }
}
