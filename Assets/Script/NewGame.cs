using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("StartGame", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("SampleScene");
    }
  
}
