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

        GameObject.Find("ButtonStart").GetComponent<UnityEngine.UI.Button>().interactable = false;
        GameObject.Find("ButtonPlaceBlue").GetComponent<UnityEngine.UI.Button>().interactable = false;
        GameObject.Find("ButtonPlaceRed").GetComponent<UnityEngine.UI.Button>().interactable = false;

        Debug.Log("SG: blue on game start");
        GameManager.blueBaseScript.OnGameStart();
        Debug.Log("SG: red on game start");
        GameManager.redBaseScript.OnGameStart();

        AiTrainer.Spawn();
    }
}
