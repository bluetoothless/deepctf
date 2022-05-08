using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToMapGenerationScript : MonoBehaviour
{
    [SerializeField] GameObject errorText;
    [SerializeField] Slider sliderBlueAgents;
    [SerializeField] Slider sliderRedAgents;
    [SerializeField] Slider sliderDeserts;
    [SerializeField] Slider sliderLakes;
    [SerializeField] Slider sliderAccSurfaces;

    private int nrOfBlueAgents;
    private int nrOfRedAgents;
    private int desertsPercent;
    private int lakesPercent;
    private int accSurfacesPercent;
    public void PassParametersAndChangeScene()
    {
        nrOfBlueAgents = (int)sliderBlueAgents.value;
        nrOfRedAgents = (int)sliderRedAgents.value;
        desertsPercent = (int)sliderDeserts.value;
        lakesPercent = (int)sliderLakes.value;
        accSurfacesPercent = (int)sliderAccSurfaces.value;

        if (desertsPercent + lakesPercent + accSurfacesPercent < 100) {
            PlayerPrefs.SetInt("nrOfBlueAgents", nrOfBlueAgents);
            PlayerPrefs.SetInt("nrOfRedAgents", nrOfRedAgents);
            PlayerPrefs.SetInt("desertsPercent", desertsPercent);
            PlayerPrefs.SetInt("lakesPercent", lakesPercent);
            PlayerPrefs.SetInt("accSurfacesPercent", accSurfacesPercent);

            SceneManager.LoadScene("SceneMain");
        }
        else
        {
            bool isActive = errorText.activeSelf;
            errorText.SetActive(!isActive);
        }
    }
}
