using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel, levelsPanel, settingsPanel;

    private void Start()
    {
        mainPanel.SetActive(true);    
    }

    public void Play()
    {
        LevelsManager.Instance.SelectLastUnlockedLevel();
        ScenesLoader.LoadGame();
    }

    public void ShowLevels()
    {
        mainPanel.SetActive(false);
        levelsPanel.SetActive(true);
    }

    public void HideLevels()
    {
        mainPanel.SetActive(true);
        levelsPanel.SetActive(false);
    }

    public void ShowSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void HideSettings()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
}
