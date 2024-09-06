using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playground : MonoBehaviour
{
    public ObstaclesGrid ObstaclesGrid => obstaclesGrid;
    [SerializeField] private ObstaclesGrid obstaclesGrid;

    public Ball Ball => ball;
    [SerializeField] private Ball ball;

    [SerializeField] private Sprite bonusTargetMarkSprite, goodMultiplierTargetMarkSprite, badMultiplierTargetMarkSprite;
    [SerializeField] private TargetMark[] targetMarks;

    public void SetTargetMark(int index, TargetMarkConfiguration configuration)
    {
        TargetMark targetMark = targetMarks[index];
        targetMark.Set(configuration);

        Sprite sprite;
        if(configuration.type == TargetMarkType.Bonus)
        {
            sprite = bonusTargetMarkSprite;
        }
        else
        {
            if(configuration.multiplier >= 1f)
            {
                sprite = goodMultiplierTargetMarkSprite;
            }
            else
            {
                sprite= badMultiplierTargetMarkSprite;
            }
        }

        targetMark.SetSprite(sprite);
    }

    //[ContextMenu("Find Target Marks")]
    //public void FindTargetMarks()
    //{
    //    targetMarks = FindObjectsByType<TargetMark>(FindObjectsSortMode.None);
    //    for (int i = 0; i < targetMarks.Length; i++)
    //    {
    //        targetMarks[i].name = "TargetMark " + i;
    //    }
    //}
}
