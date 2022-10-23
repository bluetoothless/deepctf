using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToMapGenerationScript : MonoBehaviour
{
    [SerializeField] GameObject errorText;
    [SerializeField] Slider sliderNrOfAgents;
    [SerializeField] Slider sliderEpisodeLength;
    [SerializeField] Slider sliderDeserts;
    [SerializeField] Slider sliderLakes;
    [SerializeField] Slider sliderAccSurfaces;
    [SerializeField] GameObject neuralNetworkSelectionButton;

    private int nrOfAgents;
    private int episodeLength;
    private int desertsPercent;
    private int lakesPercent;
    private int accSurfacesPercent;
    public void PassParametersAndChangeScene()
    {
        nrOfAgents = (int)sliderNrOfAgents.value;
        episodeLength = (int)sliderEpisodeLength.value;
        desertsPercent = (int)sliderDeserts.value;
        lakesPercent = (int)sliderLakes.value;
        accSurfacesPercent = (int)sliderAccSurfaces.value;

        if (desertsPercent + lakesPercent + accSurfacesPercent < 100) {
            PlayerPrefs.SetInt("nrOfAgents", nrOfAgents);
            PlayerPrefs.SetInt("episodeLength", episodeLength);
            PlayerPrefs.SetInt("desertsPercent", desertsPercent);
            PlayerPrefs.SetInt("lakesPercent", lakesPercent);
            PlayerPrefs.SetInt("accSurfacesPercent", accSurfacesPercent);
            PlayerPrefs.SetString("neuralNetworkPath", 
                neuralNetworkSelectionButton.GetComponent<SelectedNeuralNetworkScript>().GetPath());

            SceneManager.LoadScene("SceneMain");
        }
        else
        {
            bool isActive = errorText.activeSelf;
            errorText.SetActive(!isActive);
        }
    }
}
