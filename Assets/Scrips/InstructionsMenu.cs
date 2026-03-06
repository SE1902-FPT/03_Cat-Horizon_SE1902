using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsMenu : MonoBehaviour
{
    [Header("Navigation")]
    public string mainMenuSceneName = "MainMenu"; // Điền tên Scene Main Menu của bạn vào đây

    public void BackToMainMenu()
    {
        // Tải Scene Main Menu
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
