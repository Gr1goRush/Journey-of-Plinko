using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesRow : MonoBehaviour
{
    private float moveDistance, moveSpeed, moveDirection = 1f, movePosition = 0f;
    private bool isMoving = false;

    private Obstacle[] obstacles;

    public void Initialize(Obstacle obstaclePrefab, ObstaclesRowConfiguration configurations, float spacing, int rowIndex)
    {
        Vector2 obstacleSize = obstaclePrefab.GetSpriteSize();

        float horizontalSize = (obstacleSize.x * configurations.obstaclesCount) + (spacing * (configurations.obstaclesCount - 1));
        float leftX = -(horizontalSize / 2);

        obstacles = new Obstacle[configurations.obstaclesCount];
        for (int i = 0; i < obstacles.Length; i++)
        {
            Obstacle obstacle = Instantiate(obstaclePrefab, transform);
            obstacle.SetPosition(new ObstaclePosition { row = rowIndex, column = i });

            float x = leftX + (obstacleSize.x / 2f) + (i * obstacleSize.x) + (i * spacing);
            obstacle.transform.localPosition = new Vector3(x, 0, 0);

            obstacles[i] = obstacle;
        }
    }

    public void StartMove(float distance, float speed)
    {
        moveDistance = distance;
        moveSpeed = speed;
        moveDirection = Random.Range(0, 2) == 0 ? 1 : -1;
        isMoving = true;

        StartCoroutine(Moving());
    }

    IEnumerator Moving()
    {
        while(isMoving)
        {
            yield return new WaitForFixedUpdate();

            movePosition += moveDirection * moveSpeed * Time.fixedDeltaTime;
            transform.localPosition = new Vector3(movePosition, transform.localPosition.y, 0f);

            if(Mathf.Abs(movePosition) >= moveDistance)
            {
                moveDirection *= -1;
            }
        }
    }

    public Obstacle GetObstacle(int index)
    {
        if(index >= obstacles.Length)
        {
            return null;
        }

        return obstacles[index];
    }
}
