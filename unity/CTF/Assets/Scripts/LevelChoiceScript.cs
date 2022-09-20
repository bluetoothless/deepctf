using System.Collections.Generic;
using UnityEngine;

public class LevelChoiceScript : MonoBehaviour
{
    void Start()
    {
        var dropdownList = GetComponent<TMPro.TMP_Dropdown>();
        dropdownList.options.Clear();
        List<string> options = new List<string>
        {
            "Level 1",
            "Level 2",
            "Level 3",
            "Level 4",
            "Level 5"
        };

        foreach (var option in options)
        {
            dropdownList.options.Add(new TMPro.TMP_Dropdown.OptionData(option));
        }

        dropdownList.value = 0; //index of default element;
    }
}
