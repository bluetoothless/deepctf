using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    public void SpectatorModeSelected()
    {
        if(SceneManager.GetActiveScene().name == "SceneMain")
        {
            GameManager.EnvContr.Ending();
        }
        GameManager.IsSpectatorMode = true;
        SceneManager.LoadScene("SceneUIMapGeneration");
    }
    public void LearningModeSelected()
    {
        GameManager.IsSpectatorMode = false;
        SceneManager.LoadScene("SceneUILearningOptions");
    }
    public void GoToMainMenu()
    {
        if (SceneManager.GetActiveScene().name == "SceneMain")
        {
            GameManager.EnvContr.Ending();
        }
        // GameManager.RedAgents = new List<GameObject> { };
        // GameManager.BlueAgents = new List<GameObject> { };

        SceneManager.LoadScene("SceneUIStart");
    }
    public void GoToMainScene()
    {
        if (SceneManager.GetActiveScene().name == "SceneMain")
        {
            GameManager.EnvContr.Ending();
        }
        SceneManager.LoadScene("SceneMain");
    }
}