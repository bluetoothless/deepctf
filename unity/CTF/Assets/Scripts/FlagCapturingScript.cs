using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCapturingScript : MonoBehaviour
{
    [SerializeField] string ownColor;
    [SerializeField] GameObject FlagInBase;
    private void OnTriggerEnter(Collider colidingObject)
    {
        if (colidingObject.tag == "Agent")
        {
            GameObject AgentFlag = colidingObject.GetComponent<AgentComponentsScript>().AgentFlag;

            //jeœli (agent dotknie nieswojej bazy, nie trzyma flagi przeciwnika, a flagas jest w bazie) 
            if (colidingObject.GetComponent<AgentComponentsScript>().color != ownColor && AgentFlag.activeSelf == false && FlagInBase.activeSelf)
            {
                bool isActive = FlagInBase.activeSelf;
                FlagInBase.SetActive(!isActive);
                isActive = AgentFlag.activeSelf;
                AgentFlag.SetActive(!isActive);
            }
            // jeœli agent dotknie swojej bazy i trzyma flagê przeciwnika
            else if (colidingObject.GetComponent<AgentComponentsScript>().color == ownColor && AgentFlag.activeSelf == true)
            {
                bool isActive = AgentFlag.activeSelf;
                AgentFlag.SetActive(!isActive);
                // wygrana dru¿yny
                Debug.Log("Team " + colidingObject.GetComponent<AgentComponentsScript>().color + " wins!");
            }
        }
    }
}
