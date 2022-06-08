using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnFlagScript : MonoBehaviour
{
    [SerializeField] GameObject FlagInOtherBase;
    [SerializeField] GameObject OwnFlagInOtherBase;
    private void passTheFlag(GameObject object1, GameObject object2)
    {
        bool isActive = object1.activeSelf;
        object1.SetActive(!isActive);
        isActive = object2.activeSelf;
        object2.SetActive(!isActive);
    }
    private void win(string color, GameObject object1, GameObject object2)
    {
        passTheFlag(object1, object2);
        // wygrana dru¿yny
        Debug.Log("Team " + color + " wins!");
    }
    private void win(string agentColor, GameObject object1)
    {
        string color = agentColor == "blue" ? "red" : "blue";
        bool isActive = object1.activeSelf;
        object1.SetActive(!isActive);
        // wygrana dru¿yny
        Debug.Log("Team " + color + " wins!");
    }
    public void returnFlagFromBase(Collider collidingObject, GameObject EnemyFlagInBase)
    {
        if (FlagInOtherBase.activeSelf)// je¿eli agent dotknie nieswojej bazy, w³asna flaga jest w bazie przeciwnika, a flaga przeciwnika jest we w³asnej bzaie
        {
            win(collidingObject.GetComponent<AgentComponentsScript>().color, EnemyFlagInBase, FlagInOtherBase);
        }
        else // je¿eli agent dotknie nieswojej bazy, w³asna flaga jest w bazie przeciwnika, a flaga przeciwnika nie jest we w³asnej bazie
        {
            passTheFlag(EnemyFlagInBase, OwnFlagInOtherBase);
        }
    }

    public void returnFlagFromAgent(GameObject agentFlag, string agentColor)
    {
        if (FlagInOtherBase.activeSelf)
        {
            win(agentColor, FlagInOtherBase);
        }
        else
        {
            passTheFlag(agentFlag, OwnFlagInOtherBase);
        }
    }
}
