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
            writer.WriteLine("gameWon_team:" + ((double)1.0f).ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("gameLost_team:" + ((double)-1.0f).ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("flagCaptured:" + ((double)0.35f).ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("flagCaptured_team:" + ((double)0.35f).ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("flagDelivered:" + ((double)0.8f).ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("flagDelivered_team:" + ((double)0.8f).ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("flagRetrievedFromAgent:" + ((double)0.5f).ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("flagRetrievedFromAgent_team:" + ((double)0.5f).ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("flagStolenFromAgent:" + ((double)-0.5f).ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("flagStolenFromAgent_team:" + ((double)-0.5f).ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("agentDead:" + ((double)-1.0f).ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("agentDead_team:" + ((double)-1.0f).ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("agentCloseToFlag:" + ((double)0.03f).ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("agentCloseToFlag_team:" + ((double)0.03f).ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("agentTouchesAgentSameColor:" + ((double)-0.001f).ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("agentTouchesAgentSameColor_team:" + ((double)-0.001f).ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("agentTouchesWall:" + ((double)-0.01f).ToString(CultureInfo.InvariantCulture));
            writer.WriteLine("agentTouchesWall_team:" + ((double)-0.01f).ToString(CultureInfo.InvariantCulture));
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