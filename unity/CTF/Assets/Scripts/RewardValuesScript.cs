using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

[Serializable]
public static class RewardValuesScript
{
    public static Dictionary<string, float> rewards;

    public static void saveRewardValues()
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
            writer.WriteLine("agentCloseToFlag:" + 10.0f);
            writer.WriteLine("agentTouchesAgentSameColor:" + -10.0f);
            writer.WriteLine("agentTouchesWall:" + -10.0f);
            writer.Close();
        }
        
    }

    public static void getRewardValues()
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
            readRewards.Add(pair[0], float.Parse(pair[1], CultureInfo.InvariantCulture));
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