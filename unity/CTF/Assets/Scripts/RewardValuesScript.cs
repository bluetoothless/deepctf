using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class RewardValuesScript : MonoBehaviour
{
    public Dictionary<string, float> rewards;

    public void saveRewardValues()
    {
        var path = Application.dataPath + "/Rewards.txt";
        StreamReader reader = new StreamReader(path);
        var text = reader.ReadToEnd();
        reader.Close();
        if (text == "")
        {
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine("gameWon:" + 1000.0f);
            writer.WriteLine("gameLost:" + -1000.0f);
            writer.WriteLine("flagCaptured:" + 50.0f);
            writer.WriteLine("flagDelivered:" + 50.0f);
            writer.WriteLine("flagRetrievedFromAgent:" + 50.0f);
            writer.WriteLine("flagStolenFromAgent:" + -50.0f);
            writer.WriteLine("agentDead:" + -1000.0f);
            writer.Close();
        }
        
    }

    public void getRewardValues()
    {
        saveRewardValues();

        List<string> lines = new List<string>();
        string path = Application.dataPath + "/Rewards.txt";
        StreamReader reader = new StreamReader(path);

        string oneLine;
        while ((oneLine = reader.ReadLine()) != null)
        {
            lines.Add(oneLine);
        }

        Dictionary<string, float> readRewards = new Dictionary<string, float>();
        foreach (string line in lines)
        {
            string[] pair = line.Split(':');
            readRewards.Add(pair[0], float.Parse(pair[1]));
        }
        reader.Close();
        rewards = readRewards;
    }
}

/*  READING VALUES
        var rewardValues = gameObject.GetComponent<RewardValuesScript>();
        rewardValues.getRewardValues();
        AddRewardAgent(rewardValues.rewards["gameLost"]);
*/