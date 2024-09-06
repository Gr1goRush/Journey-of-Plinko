using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public ObstaclePosition Position { get; private set; }

    [SerializeField] private SpriteRenderer spriteRenderer;

    public Vector3 GetSpriteSize()
    {
        return spriteRenderer.bounds.size;
    }

    public void SetPosition(ObstaclePosition obstaclePosition)
    {
        Position = obstaclePosition;
        name = "Obstacle " + Position.row + " " + Position.column;
    }
}
