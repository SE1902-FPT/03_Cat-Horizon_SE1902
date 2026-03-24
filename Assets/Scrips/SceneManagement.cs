using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Story");
    }

    public void Level1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
    }

    public void Level3()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level3");
    }

    public void Level()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ChooseLevel");
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Introduction()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Introduction");
    }

    public void HighScore()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HighScore");
    }

    public void AboutUs()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("AboutUs");
    }

    public void Setting()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Setting");
    }

    public void GameOver()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOver");
    }

    public void Story2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Story2");
    }

    public void Story3()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Story3");
    }

    public void FinalStory()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("FinalStory");
    }
}
