using System;
using System.IO;
using UnityEngine;

[Serializable]
public class RewardValuesScript:MonoBehaviour
{
    public float gameWon;
    public float flagCaptured;
    public float flagRetrievedFromAgent;
    public float gameLost;
    public float agentDead;

    public void saveRewardValues()
    {
        var jsonRewards = JsonUtility.ToJson(this);
        var path = Application.dataPath + "/Rewards.json";
        File.WriteAllText(path, jsonRewards);
    }

    public RewardValuesScript getRewardValues()
    {
        var jsonRewards = File.ReadAllText(Application.dataPath + "/Rewards.json");
        var rewardValues = JsonUtility.FromJson<RewardValuesScript>(jsonRewards);
        return rewardValues;
    }
}

/*  READING VALUES FROM JSON
        var rewardValues = new RewardValuesScript().getRewardValues();
        var exampleNeededValue = rewardValues.gameLost;
*/

/*  SAVING VALUES TO JSON
        var rewardValues = new RewardValuesScript
        {
            gameWon = 1.0F,
            flagCaptured = 0.2F,
            flagRetrievedFromAgent = 0.2F,
            gameLost = 1.0F,
            agentDead = 0.9F
        };
        rewardValues.saveRewardValues();
*/
