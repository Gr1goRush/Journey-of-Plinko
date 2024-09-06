using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class LevelsPanel : MonoBehaviour
{
    [SerializeField] private LevelsPanelButton[] buttons;

    void Start()
    {
        for (int levelIndex = 0; levelIndex < buttons.Length; levelIndex++)
        {
            LevelsPanelButton levelsPanelButton = buttons[levelIndex];
            LevelState levelState = LevelsManager.Instance.GetLevelState(levelIndex);

            if (levelState.unlocked)
            {
                levelsPanelButton.SetUnlocked();
            }
            else
            {
                levelsPanelButton.SetPrice(LevelsManager.Instance.LevelPrice);
            }

            int temp = levelIndex;
            levelsPanelButton.AddClickListener(() => SelectLevel(temp));
        }
    }

    private void SelectLevel(int levelIndex)
    {
        LevelState levelState = LevelsManager.Instance.GetLevelState(levelIndex);

        if (levelState.unlocked)
        {
           LevelsManager.Instance.SelectLevel(levelIndex);
            ScenesLoader.LoadGame();
        }
        else
        {
            if (LevelsManager.Instance.TryBuyLevel(levelIndex))
            {
                buttons[levelIndex].SetUnlocked();
            }
        }

    }
}
