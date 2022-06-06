using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        var blueBaseScript = GameObject.Find("Blue Base(Clone)").GetComponent<BlueBaseScript>();
        var redBaseScript = GameObject.Find("Red Base(Clone)").GetComponent<RedBaseScript>();

        blueBaseScript.OnGameStart();
        redBaseScript.OnGameStart();
    }
}
