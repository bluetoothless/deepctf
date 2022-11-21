using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownListScript : MonoBehaviour
{
    public string dropdownListType;

    void Start()
    {
        var dropdownList = GetComponent<TMPro.TMP_Dropdown>();
        dropdownList.options.Clear();
        List<string> options = new List<string>();
        if (dropdownListType == "level")
        {
            options.AddRange(new List<string>
            {
                "Level 1",
                "Level 2",
                "Level 3"
            });
        }
        else if (dropdownListType == "levelOrFreeMode")
        {
            options.AddRange(new List<string>
            {
                "Level 1",
                "Level 2",
                "Level 3",
                "Team vs. Team"
            });
        }
        else if (dropdownListType == "mapType")
        {
            options.AddRange(new List<string>
            {
                "Grass only",
                "Grass, Accelerate, Desert",
                "Grass, Water",
                "All bioms"
            });
        }

        foreach (var option in options)
        {
            dropdownList.options.Add(new TMPro.TMP_Dropdown.OptionData(option));
        }

        dropdownList.value = 0; //index of default element;
    }
    public void OnChange()
    {

    }
}
