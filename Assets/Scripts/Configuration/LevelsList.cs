using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public struct LevelData
{
  //  public int price;
}

[CreateAssetMenu(menuName = "Game Data/Levels List", fileName = "LevelsList", order = 1)]
public class LevelsList : ScriptableObject
{
    public int levelsCount = 15, levelPrice = 1000;
   // public LevelData[] levels;
}