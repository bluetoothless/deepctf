using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventTriggerScript : MonoBehaviour
{
    public Text label;
    public Toggle toggle;

    public void WhoAmI()
    {
        if (!label || !toggle)
        {
            Debug.LogError("please assign both the label and the toggle in the inspector");
            return;
        }

        Debug.LogFormat("label: {0}    toggle.isOn: {1}", label.text, toggle.isOn);
    }
}
