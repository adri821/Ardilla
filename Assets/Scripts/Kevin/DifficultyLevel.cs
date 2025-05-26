using UnityEngine;

public class DifficultyLevel : MonoBehaviour
{
    public static DifficultyLevel instance;

    public static bool levelHard = false;

    private void Awake() {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);
    }
}
