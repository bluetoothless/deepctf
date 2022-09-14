using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvController : MonoBehaviour
{
    public static int steps = -1;
    public int maxSteps;

    private PerlinNoiseMapGeneration mapGenerator;

    private int errorBrakAgentowCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.EnvContr = this;

        mapGenerator = transform.GetChild(3).GetComponent<PerlinNoiseMapGeneration>(); 
        if(mapGenerator == null)
        {
            Debug.LogError("mapGenerator is empty!");
        }
        mapGenerator.StartAndGeneration(); // use AITRAINER, PLACES BASES AND TILES

        StartGameScript SGS = transform.GetChild(7).GetChild(0).GetChild(0).GetChild(3).GetComponent<StartGameScript>();
        if (mapGenerator == null)
        {
            Debug.LogError("StartGameScript SGS is empty!");
        }
        SGS.StartGame(); //StartGameScript! SET AgentGroup in GameManager, set buttons, BaseScript.OnGameStart(), AiTrainer.Spawn()->move BlueAgents Up;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if((!GameManager.IsAnyRed() || !GameManager.IsAnyBlue()))
        {
            errorBrakAgentowCounter++;
            if (errorBrakAgentowCounter > 20)
            {
                Debug.LogError("ERROR KURWAA: brak agentów, steps: " + steps);
            }
        }
        if (steps % 100 == 0)
        {
            UnityEngine.Debug.Log("Steps:" + steps);
        }
        if (steps > maxSteps)
        {
            steps = 0;
            GameManager.EndMaxSteps();
            return;
        }
        steps++;
    }

    public int GetSteps()
    {
        return steps;
    }
}
