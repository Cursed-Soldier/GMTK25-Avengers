using UnityEngine;
using UnityEngine.SceneManagement;

public class PressEnterToNextScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Return is the Enter key
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if there is a next scene in the build
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No next scene in build settings!");
        }
    }
}
