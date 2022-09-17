using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    public void SpectatorModeSelected()
    {
        GameManager.IsSpectatorMode = true;
        SceneManager.LoadScene("SceneUIMapGeneration");
    }
    public void LearningModeSelected()
    {
        GameManager.IsSpectatorMode = false;
        //SceneManager.LoadScene("");
    }
    public void GoToMainMenu()
    {
        GameManager.RedAgents = new List<GameObject> { };
        GameManager.BlueAgents = new List<GameObject> { };
        SceneManager.LoadScene("SceneUIStart");
    }
    public void GoToMainScene()
    {
        SceneManager.LoadScene("SceneMain");
    }
}