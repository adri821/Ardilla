using UnityEngine;

public class ScoreButton : MonoBehaviour
{
    public void ScoreSend() {
        ScoreManager.Instance.GuardarDesdeUI();
    }
}
