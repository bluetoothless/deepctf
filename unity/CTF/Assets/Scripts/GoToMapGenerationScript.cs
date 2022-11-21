using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToMapGenerationScript : MonoBehaviour
{
    [SerializeField] GameObject biomesPercentErrorText;
    [SerializeField] GameObject noNeuralNetworkPathErrorText;
    [SerializeField] Slider sliderNrOfAgents;
    [SerializeField] Slider sliderEpisodeLength;
    [SerializeField] Slider sliderDeserts;
    [SerializeField] Slider sliderLakes;
    [SerializeField] Slider sliderAccSurfaces;
    [SerializeField] GameObject neuralNetworkSelectionButton;
    [SerializeField] GameObject levelChoiceDropdownListComponent;
    private TMPro.TMP_Dropdown levelChoiceDropdownList;

    private int nrOfAgents;
    private int episodeLength;
    private int desertsPercent;
    private int lakesPercent;
    private int accSurfacesPercent;
    public void PassParametersAndChangeScene()
    {
        nrOfAgents = (int)sliderNrOfAgents.value;
        episodeLength = (int)sliderEpisodeLength.value;
        levelChoiceDropdownList = levelChoiceDropdownListComponent.GetComponent<TMPro.TMP_Dropdown>();
        var level = levelChoiceDropdownList.value;
        desertsPercent = (int)sliderDeserts.value;
        lakesPercent = (int)sliderLakes.value;
        accSurfacesPercent = (int)sliderAccSurfaces.value;

        if (desertsPercent + lakesPercent + accSurfacesPercent < 100) {
            PlayerPrefs.SetInt("nrOfAgents", nrOfAgents);
            PlayerPrefs.SetInt("episodeLength", episodeLength);
            PlayerPrefs.SetInt("desertsPercent", desertsPercent);
            PlayerPrefs.SetInt("lakesPercent", lakesPercent);
            PlayerPrefs.SetInt("accSurfacesPercent", accSurfacesPercent);
            PlayerPrefs.SetInt("level", level);

            var nnPath = neuralNetworkSelectionButton.GetComponent<SelectedNeuralNetworkScript>().GetPath();
            if (nnPath.Contains(".onnx"))
            {
                PlayerPrefs.SetString("neuralNetworkPath", nnPath);

                SceneManager.LoadScene("SceneMain");
            }
            else
            {
                bool isActive = noNeuralNetworkPathErrorText.activeSelf;
                noNeuralNetworkPathErrorText.SetActive(!isActive);
            }
        }
        else
        {
            bool isActive = biomesPercentErrorText.activeSelf;
            biomesPercentErrorText.SetActive(!isActive);
        }
    }
}
