using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    public void SpectatorModeSelected()
    {
        SceneManager.LoadScene("SceneUIMapGeneration");
    }
    public void LearningModeSelected()
    {
        //SceneManager.LoadScene("");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("SceneUIStart");
    }
    public void GoToMainScene()
    {
        SceneManager.LoadScene("SceneMain");
    }
}