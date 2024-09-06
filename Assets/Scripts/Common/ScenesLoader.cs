using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesLoader
{
    public static void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public static void LoadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}