using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class StartGameScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        Debug.Log("SG: ");
        var blueBaseScript = GameObject.Find("Blue Base(Clone)").GetComponent<BlueBaseScript>();
        var redBaseScript = GameObject.Find("Red Base(Clone)").GetComponent<RedBaseScript>();

        GameObject.Find("ButtonStart").GetComponent<UnityEngine.UI.Button>().interactable = false;
        GameObject.Find("ButtonPlaceBlue").GetComponent<UnityEngine.UI.Button>().interactable = false;
        GameObject.Find("ButtonPlaceRed").GetComponent<UnityEngine.UI.Button>().interactable = false;

        Debug.Log("SG: blue on game start");
        blueBaseScript.OnGameStart();
        Debug.Log("SG: red on game start");
        redBaseScript.OnGameStart();

        GameManager.redAgentGroup = redBaseScript.m_AgentGroup;
        GameManager.blueAgentGroup = blueBaseScript.m_AgentGroup;

        AiTrainer.Spawn();
    }
}
