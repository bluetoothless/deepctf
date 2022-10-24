using UnityEngine;
using UnityEngine.UI;

public class LearningOptionsScript : MonoBehaviour
{
    public GameObject dropdownListComponent;
    public Slider sliderNrOfEnvInstances;
    private TMPro.TMP_Dropdown dropdownList;
    private int nrOfEnvInstances;
    private string level;
    void Start()
    {
        dropdownList = dropdownListComponent.GetComponent<TMPro.TMP_Dropdown>();
    }

    public void StartLearning()
    {
        nrOfEnvInstances = (int)sliderNrOfEnvInstances.value;
        level = dropdownList.options[dropdownList.value].text;

        Debug.Log("Level: "+ level + "\nNumber of learning environment instances: " + nrOfEnvInstances);
        Runner.Execute();
    }
    public void FinishLearning()
    {
        Runner.Finish();
    }
}
