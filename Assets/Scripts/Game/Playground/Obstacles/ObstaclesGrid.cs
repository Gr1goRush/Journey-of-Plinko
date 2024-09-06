using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesGrid : MonoBehaviour
{
    [SerializeField] private Vector2 spacings;

    [SerializeField] private Obstacle obstaclePrefab;
    [SerializeField] private ObstacleBlock obstacleBlockPrefab;

    private ObstaclesRow[] rows;

//#if UNITY_EDITOR
//    public ObstacleBlockConfiguration[] obstacleBlocksConfigurations;
//#endif

    public void Initialize(ObstaclesRowConfiguration[] configurations, ObstacleBlockConfiguration[] obstacleBlocksConfigurations)
    {
        Vector3 obstacleSize = obstaclePrefab.GetSpriteSize();

        rows = new ObstaclesRow[configurations.Length];
        for (int i = 0; i < rows.Length; i++)
        {
            GameObject obj = new GameObject("Row " + i);
            obj.transform.SetParent(transform, false);

            float y = (rows.Length - i - 1) * (spacings.y + obstacleSize.y);
            obj.transform.localPosition = new Vector3(0, y, 0);

            ObstaclesRow obstaclesRow = obj.AddComponent<ObstaclesRow>();
            rows[i] = obstaclesRow;

            obstaclesRow.Initialize(obstaclePrefab, configurations[i], spacings.x, i);

            if (configurations[i].dynamic)
            {
                obstaclesRow.StartMove(configurations[i].moveDistance, configurations[i].moveSpeed);
            }
        }

        Destroy(obstaclePrefab.gameObject);
        obstaclePrefab = null;

        if (obstacleBlocksConfigurations != null)
        {
            for (int i = 0; i < obstacleBlocksConfigurations.Length; i++)
            {
                ObstacleBlockConfiguration obstacleBlockConfiguration = obstacleBlocksConfigurations[i];

                ObstacleBlock obstacleBlock = Instantiate(obstacleBlockPrefab, obstacleBlockPrefab.transform.parent);
                if (obstacleBlockConfiguration.blinking)
                {
                    obstacleBlock.StartBlinking();
                }

                ObstaclePosition start = obstacleBlockConfiguration.startObstacle;
                ObstaclePosition end = obstacleBlockConfiguration.endObstacle;

                obstacleBlock.name = "Obstacle Block " + start.row + " " + start.column + " " + end.row + " " + end.column;

                Obstacle startObstacle = GetObstacle(start);
                if(startObstacle == null)
                {
                    continue;
                }
                Obstacle endObstacle = GetObstacle(end);
                if (endObstacle == null)
                {
                    continue;
                }

                if (start.row == end.row)
                {
                    obstacleBlock.transform.SetParent(rows[start.row].transform);
                }

                obstacleBlock.transform.position = Vector3.Lerp(startObstacle.transform.position, endObstacle.transform.position, 0.5f);
                obstacleBlock.transform.rotation = Utility.GetLookRotation(startObstacle.transform.position, endObstacle.transform.position);
            }
        }

        Destroy(obstacleBlockPrefab.gameObject);
        obstacleBlockPrefab = null;
    }

    private Obstacle GetObstacle(int row, int column)
    {
        if(rows.Length <= row)
        {
            return null;
        }
        return rows[row].GetObstacle(column);
    }

    private Obstacle GetObstacle(ObstaclePosition obstaclePosition)
    {
        return GetObstacle(obstaclePosition.row, obstaclePosition.column);
    }

//#if UNITY_EDITOR
//    [ContextMenu("Add Obstacle Block")]
//    public void AddObstacleBlock()
//    {
//        GameObject[] selectedGameObjects = UnityEditor.Selection.gameObjects;

//        Obstacle obstacle1 = selectedGameObjects[0].GetComponent<Obstacle>();
//        Obstacle obstacle2 = selectedGameObjects[1].GetComponent<Obstacle>();

//        List<ObstacleBlockConfiguration> obstacleBlockConfigurationsList = new List<ObstacleBlockConfiguration>(obstacleBlocksConfigurations)
//        {
//            new ObstacleBlockConfiguration
//            {
//                startObstacle = obstacle1.Position,
//                endObstacle = obstacle2.Position,
//            }
//        };

//        obstacleBlocksConfigurations = obstacleBlockConfigurationsList.ToArray();
//    }
//#endif
}
