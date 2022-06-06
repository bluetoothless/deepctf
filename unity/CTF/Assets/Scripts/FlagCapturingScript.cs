using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCapturingScript : MonoBehaviour
{
    [SerializeField] string baseColor;
    [SerializeField] GameObject FlagInBase;
    [SerializeField] GameObject EnemyFlagInBase;
    private GameObject FlagInOtherBase;

    private void passTheFlag(GameObject object1, GameObject object2) {
        bool isActive = object1.activeSelf;
        object1.SetActive(!isActive);
        isActive = object2.activeSelf;
        object2.SetActive(!isActive);
    }
    private void win (string color, GameObject object1)
    {
        bool isActive = object1.activeSelf;
        object1.SetActive(!isActive);
        // wygrana drużyny
        Debug.Log("Team " + color + " wins!");
    }
    private void win(string color, GameObject object1, GameObject object2)
    {
        passTheFlag(object1, object2);
        // wygrana drużyny
        Debug.Log("Team " + color + " wins!");
    }

    private void OnTriggerEnter(Collider colidingObject)
    {
        if (colidingObject.tag == "Agent")
        {
            if (baseColor == "blue") 
                FlagInOtherBase = GameObject.Find("Red Base(Clone)/RedFlag");
            else
                FlagInOtherBase = GameObject.Find("Blue Base(Clone)/BlueFlag");

            GameObject AgentFlag = colidingObject.GetComponent<AgentComponentsScript>().AgentFlag;
            bool agentColorEqualsBaseColor = colidingObject.GetComponent<AgentComponentsScript>().color == baseColor;
            bool agentHoldsEnemyFlag = AgentFlag.activeSelf;
            

            if (agentColorEqualsBaseColor)
            {
                if (!agentHoldsEnemyFlag) 
                    return;

                if (FlagInBase.activeSelf) // jeżeli agent dotknie swojej bazy, trzyma flagę przeciwnika, a flaga jest w bazie
                {
                    win(colidingObject.GetComponent<AgentComponentsScript>().color, AgentFlag);
                }
                else // jeżeli agent dotknie swojej bazy, trzyma flagę przeciwnika, a flagi nie ma w bazie
                {
                    passTheFlag(EnemyFlagInBase, AgentFlag);
                }
            }
            else
            {
                if (!AgentFlag.activeSelf && FlagInBase.activeSelf) // jeżeli agent dotknie nieswojej bazy, nie trzyma flagi przeciwnika, a flaga przeciwnika jest w bazie
                {
                    passTheFlag(FlagInBase, AgentFlag);
                }
                else if (EnemyFlagInBase.activeSelf) 
                {
                    if (FlagInOtherBase.activeSelf)// jeżeli agent dotknie nieswojej bazy, własna flaga jest w bazie przeciwnika, a flaga przeciwnika jest we własnej bzaie
                    {
                        win(colidingObject.GetComponent<AgentComponentsScript>().color, EnemyFlagInBase, FlagInOtherBase);
                    }
                    else // jeżeli agent dotknie nieswojej bazy, własna flaga jest w bazie przeciwnika, a flaga przeciwnika nie jest we własnej bazie
                    {
                        passTheFlag(EnemyFlagInBase, FlagInOtherBase);
                    }
                }
            }


            /*
            //je�li agent dotknie nieswojej bazy, nie trzyma flagi przeciwnika, a flaga przeciwnika jest w bazie 
            if (colidingObject.GetComponent<AgentComponentsScript>().color != baseColor && AgentFlag.activeSelf == false && FlagInBase.activeSelf == true)
            {
                bool isActive = FlagInBase.activeSelf;
                FlagInBase.SetActive(!isActive);
                isActive = AgentFlag.activeSelf;
                AgentFlag.SetActive(!isActive);
            }
            //je�li agent dotknie nieswojej bazy, a w�asna flaga jest w bazie 
            else if (colidingObject.GetComponent<AgentComponentsScript>().color != baseColor && AgentFlag.activeSelf == false && EnemyFlagInBase.activeSelf == true)
            {
                bool isActive = EnemyFlagInBase.activeSelf;
                EnemyFlagInBase.SetActive(!isActive);
                isActive = FlagInBase.activeSelf;
                FlagInBase.SetActive(!isActive);
            }
            // je�li agent dotknie swojej bazy, trzyma flag� przeciwnika, a flagi nie ma w bazie
            else if (colidingObject.GetComponent<AgentComponentsScript>().color == baseColor && AgentFlag.activeSelf == true && FlagInBase.activeSelf == false)
            {
                bool isActive = EnemyFlagInBase.activeSelf;
                EnemyFlagInBase.SetActive(!isActive);
                isActive = AgentFlag.activeSelf;
                AgentFlag.SetActive(!isActive);
            }
            // je�li agent dotknie swojej bazy, trzyma flag� przeciwnika, a flaga jest w bazie
            else if (colidingObject.GetComponent<AgentComponentsScript>().color == baseColor && AgentFlag.activeSelf == true && FlagInBase.activeSelf == true)
            {
                bool isActive = AgentFlag.activeSelf;
                AgentFlag.SetActive(!isActive);
                // wygrana dru�yny
                Debug.Log("Team " + colidingObject.GetComponent<AgentComponentsScript>().color + " wins!");
            }*/
        }
    }
}
