using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    // Make sure there is only one instance of this script
    private static CanvasManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // Keep this game object when loading new scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Destroy any duplicate game objects
            Destroy(gameObject);
        }
    }

    // Load a new scene by name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
