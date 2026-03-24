using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public void RestartLevel()
    {
        Time.timeScale = 1; // Nhớ đặt lại Time.timeScale về 1 trước khi reload
        // Load scene hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}