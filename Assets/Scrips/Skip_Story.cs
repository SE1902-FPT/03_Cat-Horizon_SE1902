using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip_Story : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Skip()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Load_Story()
    {
        SceneManager.LoadScene("Story_teller");
    }
}
