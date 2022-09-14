using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvController : MonoBehaviour
{
    [HideInInspector]
    public int steps = -1;
    public int maxSteps;

    public int NumberOfAgents = 4;

    private PerlinNoiseMapGeneration mapGenerator;

    private bool isEverythingSet;

    void Awake()//false dla testow czy initialize wszystko
    {
        isEverythingSet = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.EnvContr = this;

        mapGenerator = transform.GetChild(3).GetComponent<PerlinNoiseMapGeneration>(); 
        while(mapGenerator == null)
        {
            Debug.LogError("mapGenerator is empty!");
        }
        mapGenerator.StartAndGeneration(); // use AITRAINER, PLACES BASES AND TILES

        BaseBaseScript blueBaseScript = GameObject.Find("Blue Base(Clone)").GetComponent<BlueBaseScript>();
        BaseBaseScript redBaseScript = GameObject.Find("Red Base(Clone)").GetComponent<RedBaseScript>();

        while(blueBaseScript == null || redBaseScript == null)
        {
            Debug.LogError("xBaseScripts is empty is empty!");
        }

        GameManager.redBaseScript = redBaseScript;
        GameManager.blueBaseScript = blueBaseScript;
        GameManager.redAgentGroup = redBaseScript.m_AgentGroup;
        GameManager.blueAgentGroup = blueBaseScript.m_AgentGroup;


        StartGameScript SGS = transform.GetChild(7).GetChild(0).GetChild(0).GetChild(3).GetComponent<StartGameScript>();
        while(mapGenerator == null)
        {
            Debug.LogError("StartGameScript SGS is empty!");
        }
        SGS.StartGame(); //StartGameScript! SET AgentGroup in GameManager, set buttons, BaseScript.OnGameStart(), AiTrainer.Spawn()->move BlueAgents Up;
    }

    private void  ActuallizeIsEverythingSet()
    {
        isEverythingSet = false;
        while(!mapGenerator.isMapSet)
        {
            Debug.LogError("Map is not set yet!");
            return;
        }
        if (!GameManager.redBaseScript.isBaseSet || !GameManager.blueBaseScript.isBaseSet)
        {
            Debug.LogError("xBase is not set yet!");
            return;
        }
        for(int i=0;i<NumberOfAgents;i++)
        {
            if (!GameManager.BlueAgents[i].GetComponent<AgentMovementWSAD>().isAgentSet)
            {
                Debug.LogError("BlueAgents[" + i + "] is not set yet!");
                return;
            }
            if (!GameManager.RedAgents[i].GetComponent<AgentMovementWSAD>().isAgentSet)
            {
                Debug.LogError("RedAgents["+i+"] is not set yet!");
                return;
            }
        }
        isEverythingSet = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        while(!isEverythingSet  && steps != -1)
        {
            //Debug.LogError("Not Everything set");
            ActuallizeIsEverythingSet();
        }

        if (steps % 100 == 0)
        {
            UnityEngine.Debug.Log("Steps:" + steps);
        }
        if (steps > maxSteps)
        {
            steps = 0;
            EndMaxSteps();
            return;
        }
        steps++;
    }

    public int GetSteps()
    {
        return steps;
    }

    public void Kill(GameObject agent)
    {
        RewardValuesScript.getRewardValues();
        agent.GetComponent<AgentMovementWSAD>().AddRewardAgent(RewardValuesScript.rewards["agentDead"]);
        agent.SetActive(false);
        string color = agent.GetComponent<AgentComponentsScript>().color;

        if (color == "red")
        {
            GameManager.RemoveRedAgent(agent);
        }
        else
        {
            GameManager.RemoveBlueAgent(agent);
        }
        CheckIfLost(color);
    }

    private void CheckIfLost(string color)
    {
        bool isRed = color == "red";
        bool aliveAgents = isRed ? GameManager.IsAnyRed() : GameManager.IsAnyBlue();

        if (!aliveAgents) // if all agents from team died
        {

            RewardValuesScript.getRewardValues();
            if (!isRed)
            {
                Debug.Log("Team red wins!");
                GameManager.AddRewardTeam(RewardValuesScript.rewards["gameLost"], "blue");
                GameManager.AddRewardTeam(RewardValuesScript.rewards["gameWon"], "red");
                EndEpisode();

            }
            else
            {
                Debug.Log("Team blue wins!");
                GameManager.AddRewardTeam(RewardValuesScript.rewards["gameLost"], "red");
                GameManager.AddRewardTeam(RewardValuesScript.rewards["gameWon"], "blue");
                EndEpisode();
            }
        }
    }

    public void EndEpisode()
    {
        Debug.Log("EE");
        Ending();
        Debug.Log("EE: EGE blue");
        GameManager.blueAgentGroup.EndGroupEpisode();
        Debug.Log("EE: EGE red");
        GameManager.redAgentGroup.EndGroupEpisode();
        //Odtad nowa mapa i start gry
        ResetScene();

    }

    public void EndMaxSteps()
    {
        //Debug.Log("EndMaxSteps");
        Ending();
        // Debug.Log("EMS: GPI blue");
        GameManager.blueAgentGroup.GroupEpisodeInterrupted();
        //Debug.Log("EMS: GPI  red");
        GameManager.redAgentGroup.GroupEpisodeInterrupted();
        ResetScene();
    }

    private void ResetScene()
    {
        ////Debug.Log("ResetScene()");
        ////Scene sceneMain = SceneManager.GetActiveScene();
        ////GameObject interfaceCamera = sceneMain.GetRootGameObjects()[7].gameObject;
        ////StartGameScript startGameScript = interfaceCamera.GetComponentInChildren<StartGameScript>();
        ////startGameScript.StartGame();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name); // reload the scene
    }

    private void Ending()
    {
        //Debug.Log("E: Clearing Lists");
        List<GameObject> tmp = new List<GameObject> { };
        foreach (GameObject agent in GameManager.RedAgents)
        {
            tmp.Add(agent);
        }
        GameManager.RedAgents.Clear();

        foreach (GameObject agent in GameManager.BlueAgents)
        {
            tmp.Add(agent);
        }
        GameManager.BlueAgents.Clear();
        //Debug.Log("E: Destroying agents");
        foreach (GameObject agent in tmp)
        {
            if (agent == null)
                continue;
            agent.SetActive(false);
            GameObject.Destroy(agent);
        }
    }
}
