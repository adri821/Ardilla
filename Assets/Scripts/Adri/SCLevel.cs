using UnityEngine;

public class SCLevel : MonoBehaviour
{
    public void DificultyNormal() {
        DifficultyLevel.levelHard = false;
    }

    public void DificultyHard() {
        DifficultyLevel.levelHard = true;
    }
}
