using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoBack()
    {
        // Replace "PreviousSceneName" with the name of the scene you want to go back to
        SceneManager.LoadScene("MainScene");
    }

        public void Settings()
    {
        // Replace "PreviousSceneName" with the name of the scene you want to go back to
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
