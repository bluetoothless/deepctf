using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsComponents : MonoBehaviour
{
    public bool AiTrainerMode = false;

    void Awake()
    {
        var levelOrFreeMode = PlayerPrefs.GetInt("level");
        if (levelOrFreeMode != 3)
        {
            AiTrainerMode = true;
        }
        else
        {
            AiTrainerMode = false;
        }
    }
}
