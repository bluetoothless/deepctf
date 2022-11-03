using System;
using UnityEngine;
using UnityEngine.UI;

public class LearningOptionsScript : MonoBehaviour
{
    public Slider sliderNrOfEnvInstances;
    public GameObject levelChoiceDropdownListComponent;
    public GameObject mapTypeChoiceDropdownListComponent;
    public TMPro.TMP_InputField learningCommandInputField;
    private TMPro.TMP_Dropdown levelChoiceDropdownList;
    private TMPro.TMP_Dropdown mapTypeChoiceDropdownList;
    private int nrOfEnvInstances;
    private string level;
    private string mapType;

    void Start()
    {
        levelChoiceDropdownList = levelChoiceDropdownListComponent.GetComponent<TMPro.TMP_Dropdown>();
        mapTypeChoiceDropdownList = mapTypeChoiceDropdownListComponent.GetComponent<TMPro.TMP_Dropdown>();

        var command = Runner.GetCommand();
        learningCommandInputField.text = command;
        learningCommandInputField.readOnly = true;
    }

    public void StartLearning()
    {
        SaveOptionsToFile();
        nrOfEnvInstances = (int)sliderNrOfEnvInstances.value;
        level = levelChoiceDropdownList.options[levelChoiceDropdownList.value].text;
        mapType = mapTypeChoiceDropdownList.options[mapTypeChoiceDropdownList.value].text;

        Debug.Log("Level: "+ level + "\nMap type: " + mapType + "\nNumber of learning environment instances: " + nrOfEnvInstances);
        Runner.Execute();
    }

    public void CopyToClipboard()
    {
        var textEditor = new TextEditor();
        textEditor.text = learningCommandInputField.text;
        textEditor.SelectAll();
        textEditor.Copy();
    }

    public void SaveOptionsToFile()
    {
        level = levelChoiceDropdownList.options[levelChoiceDropdownList.value].text;
        mapType = mapTypeChoiceDropdownList.options[mapTypeChoiceDropdownList.value].text;
        Debug.Log("Level choice: " + level);
        Debug.Log("Map type choice: " + mapType);
    }
}
