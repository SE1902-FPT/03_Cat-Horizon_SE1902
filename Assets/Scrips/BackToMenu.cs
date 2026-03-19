using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Reusable script — loads the MainMenu scene.
/// Attach to a Button's onClick or call GoBack() from code.
/// </summary>
public class BackToMenu : MonoBehaviour
{
    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
