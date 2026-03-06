using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipStory : MonoBehaviour
{
    public void Skip()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
