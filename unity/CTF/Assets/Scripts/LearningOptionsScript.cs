using System;
using System.Diagnostics;
using System.IO;
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

        var command = Runner.GetCommand((int)sliderNrOfEnvInstances.value);
        learningCommandInputField.text = command;
        learningCommandInputField.readOnly = true;
    }

    public void StartLearning()
    {
        SaveOptionsToFile();
        nrOfEnvInstances = (int)sliderNrOfEnvInstances.value;
        level = levelChoiceDropdownList.options[levelChoiceDropdownList.value].text;
        mapType = mapTypeChoiceDropdownList.options[mapTypeChoiceDropdownList.value].text;

        // Debug.Log("Level: "+ level + "\nMap type: " + mapType + "\nNumber of learning environment instances: " + nrOfEnvInstances);
        Runner.Execute(nrOfEnvInstances);
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
        // Debug.Log("Level choice: " + level);
        // Debug.Log("Map type choice: " + mapType);

        String path;
        if (Application.isEditor)
        {
            path = Application.streamingAssetsPath + "/LearningConfig.txt";
        }
        else
        {
            // path = Application.dataPath + "/../MainSceneBuild/CTF_Data/StreamingAssets" + "/LearningConfig.txt";
            path = Directory.GetCurrentDirectory() + "\\MainSceneBuild\\CTF_Data\\StreamingAssets" + "\\LearningConfig.txt";
        }
        // UnityEngine.Debug.Log("Path: " + path);

        FileStream fileStream = File.Open(path, FileMode.Append);
        StreamWriter writer = new StreamWriter(fileStream);
        writer.Close();

        writer = new StreamWriter(path, append:false);
        writer.WriteLine(levelChoiceDropdownList.value.ToString() + ';' + (mapTypeChoiceDropdownList.value + 1).ToString());
        writer.Close();
    }

    public void OpenRewardsFile()
    {
        var rewardsFile = Directory.GetCurrentDirectory() + "\\MainSceneBuild\\CTF_Data\\StreamingAssets" + "\\Rewards.txt";
        Process.Start("notepad.exe", rewardsFile);
    }

    public void OpenConfigFile()
    {
        var configFile = Application.streamingAssetsPath + "/configuration.yaml";
        Process.Start("notepad.exe", configFile);
    }

    public void OpenMLAgentsDocsInBrowser()
    {
        Process.Start("https://github.com/Unity-Technologies/ml-agents/blob/release_19_docs/docs/Training-Configuration-File.md");
    }

    public void UpdateCommand()
    {
        var command = Runner.GetCommand((int)sliderNrOfEnvInstances.value);
        learningCommandInputField.text = command;
    }
}
