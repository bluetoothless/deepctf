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
            writer.WriteLine("gameWon_team:" + 1.0f);
            writer.WriteLine("gameLost_team:" + -1.0f);
            writer.WriteLine("flagCaptured:" + 0.35f);
            writer.WriteLine("flagCaptured_team:" + 0.35f);
            writer.WriteLine("flagDelivered:" + 0.8f);
            writer.WriteLine("flagDelivered_team:" + 0.8f);
            writer.WriteLine("flagRetrievedFromAgent:" + 0.5f);
            writer.WriteLine("flagRetrievedFromAgent_team:" + 0.5f);
            writer.WriteLine("flagStolenFromAgent:" + -0.5f);
            writer.WriteLine("flagStolenFromAgent_team:" + -0.5f);
            writer.WriteLine("agentDead:" + -1.0f);
            writer.WriteLine("agentDead_team:" + -1.0f);
            writer.WriteLine("agentCloseToFlag:" + 0.03f);
            writer.WriteLine("agentCloseToFlag_team:" + 0.03f);
            writer.WriteLine("agentTouchesAgentSameColor:" + -0.001f);
            writer.WriteLine("agentTouchesAgentSameColor_team:" + -0.001f);
            writer.WriteLine("agentTouchesWall:" + -0.01f);
            writer.WriteLine("agentTouchesWall_team:" + -0.01f);
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