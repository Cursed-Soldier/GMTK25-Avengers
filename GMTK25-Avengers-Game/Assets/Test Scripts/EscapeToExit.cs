using UnityEngine;

public class EscapeToExit : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
