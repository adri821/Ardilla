using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader _instance;

    public void LoadSceneWithFade(string sceneName)
    {
        Fade.LoadScene(sceneName).SetFadeTime(1.5f);
    }

    public void ReloadScene() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Fade.LoadScene(currentSceneName).SetFadeTime (1.5f);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void CloseGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_STANDALONE_WIN
                Application.Quit();
	#elif UNITY_ANDROID
				Application.Quit();
    #endif
    }
}