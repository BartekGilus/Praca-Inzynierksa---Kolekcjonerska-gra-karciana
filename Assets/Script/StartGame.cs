using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    public void GameStart()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("StartGame");
    }

    public void DeckBuilder()
    {
        SceneManager.LoadScene("DeckBuilder", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("StartGame");

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartGame", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("DeckBuilder");
    }
}
