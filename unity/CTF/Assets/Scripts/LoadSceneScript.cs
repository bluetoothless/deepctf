using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpectatorModeSelected()
    {
        SceneManager.LoadScene("SceneUIMapGeneration");
    }
    public void LearningModeSelected()
    {
        //SceneManager.LoadScene("SceneUIMapGeneration");
    }
}