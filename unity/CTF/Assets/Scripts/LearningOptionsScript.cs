using UnityEngine;
using UnityEngine.UI;

public class LearningOptionsScript : MonoBehaviour
{
    public GameObject dropdownListComponent;
    public Slider sliderNrOfEnvInstances;
    public TMPro.TMP_InputField learningCommandInputField;
    private TMPro.TMP_Dropdown dropdownList;
    private int nrOfEnvInstances;
    private string level;
    void Start()
    {
        dropdownList = dropdownListComponent.GetComponent<TMPro.TMP_Dropdown>();

        var command = Runner.GetCommand();
        learningCommandInputField.text = command;
        learningCommandInputField.readOnly = true;
    }

    public void StartLearning()
    {
        nrOfEnvInstances = (int)sliderNrOfEnvInstances.value;
        level = dropdownList.options[dropdownList.value].text;

        Debug.Log("Level: "+ level + "\nNumber of learning environment instances: " + nrOfEnvInstances);
        Runner.Execute();
    }

    public void CopyToClipboard()
    {
        var textEditor = new TextEditor();
        textEditor.text = learningCommandInputField.text;
        textEditor.SelectAll();
        textEditor.Copy();
    }
}
