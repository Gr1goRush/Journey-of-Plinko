using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LevelState
{
    public bool unlocked;
}

public class LevelsManager : Singleton<LevelsManager>
{
    public int LevelsCount => levelsList.levelsCount;
    public int LevelPrice => levelsList.levelPrice;

    public int SelectedLevelIndex { get; private set; }

    [SerializeField] private LevelsList levelsList;

    private LevelState[] levelStates;

    protected override void Awake()
    {
        base.Awake();

        levelStates = new LevelState[levelsList.levelsCount];
        for (int i = 0; i < levelStates.Length; i++)
        {
            LevelState levelState = new LevelState();
            levelState.unlocked = i == 0 || PlayerSaves.GetInt("LevelUnlocked_" + i.ToString(), 0) == 1;
      //      levelState.unlocked = true;
            levelStates[i] = levelState;
        }

        SelectedLevelIndex = 0;
    }

    public LevelConfiguration LoadSelectedLevelContent()
    {
        return Resources.Load<LevelConfiguration>("Level" + (SelectedLevelIndex + 1).ToString());
    }

    public LevelState GetLevelState(int levelIndex)
    {
        return levelStates[levelIndex];
    }

    public void SelectLastUnlockedLevel()
    {
        for (int i = levelStates.Length - 1; i >= 0; i--)
        {
            if (levelStates[i].unlocked)
            {
                SelectedLevelIndex = i;
                return;
            }
        }

        SelectedLevelIndex = 0;
    }

    public void UnlockRandomLevel()
    {
        List<int> lockedLevels = new List<int>();

        for (int i = levelStates.Length - 1; i >= 0; i--)
        {
            if (!levelStates[i].unlocked)
            {
               lockedLevels.Add(i);
            }
        }

        if(lockedLevels.Count > 0 )
        {
            int levelIndex = lockedLevels[Random.Range(0, lockedLevels.Count)];
            UnlockLevel(levelIndex);
        }
    }

    private void UnlockLevel(int levelIndex)
    {
        LevelState levelState = levelStates[levelIndex];
        levelState.unlocked = true;
        levelStates[levelIndex] = levelState;

        PlayerSaves.SetInt("LevelUnlocked_" + levelIndex.ToString(), 1);
    }

    public bool TryBuyLevel(int levelIndex)
    {
        if (BalanceManager.Instance.HasBalance(LevelPrice))
        {
            BalanceManager.Instance.SubtractBalance(LevelPrice);
            UnlockLevel(levelIndex);

            return true;
        }

        return false;
    }

    public void SelectLevel(int levelIndex)
    {
        SelectedLevelIndex = levelIndex;
    }
}
