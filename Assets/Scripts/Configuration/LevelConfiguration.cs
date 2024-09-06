using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct ObstaclesRowConfiguration 
{
    public int obstaclesCount;
    public float moveDistance, moveSpeed;
    public bool dynamic;
}

[System.Serializable]
public struct ObstaclePosition
{
    public int row, column;
}

[System.Serializable]
public struct ObstacleBlockConfiguration
{
    public ObstaclePosition startObstacle, endObstacle;
    public bool blinking;
}

[System.Serializable]
public struct TargetMarkConfiguration
{
    public TargetMarkType type;
    public float multiplier;
}

[CreateAssetMenu(menuName = "Game Data/Level", fileName = "Level", order = 2)]
public class LevelConfiguration : ScriptableObject
{
    public ObstaclesRowConfiguration[] obstaclesRows;
    public ObstacleBlockConfiguration[] obstacleBlocks;
    public TargetMarkConfiguration[] targetMarks;

    [ContextMenu("Set Random Target Marks")]
    public void SetRandomTargetMarks()
    {
        float[] multipliers = new float[] { 3, 1.5f, 0.5f, 0.3f, 0.2f };
        List<float> randomMultiplies = new List<float>();
        randomMultiplies.AddRange(multipliers);
        randomMultiplies.AddRange(multipliers);
        randomMultiplies.Add(0);
        randomMultiplies = Utility.GetRandomEnumerable(randomMultiplies).ToList();

        targetMarks = new TargetMarkConfiguration[randomMultiplies.Count];
        for (int i = 0; i < targetMarks.Length; i++)
        {
            TargetMarkConfiguration targetMarkConfiguration = new TargetMarkConfiguration();
            if (randomMultiplies[i] == 0)
            {
                targetMarkConfiguration.type = TargetMarkType.Bonus;
                targetMarkConfiguration.multiplier = 0;
            }
            else
            {
                targetMarkConfiguration.type = TargetMarkType.Multiplier;
                targetMarkConfiguration.multiplier = randomMultiplies[i];
            }

            targetMarks[i] = targetMarkConfiguration;
        }
    }
}
