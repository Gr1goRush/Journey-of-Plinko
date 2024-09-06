using Assets.Scripts.Game.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    public MainPanel MainPanel => mainPanel;
    [SerializeField] private MainPanel mainPanel;

    public SuperGamePanel SuperGamePanel => superGamePanel;
    [SerializeField] private SuperGamePanel superGamePanel;

    [SerializeField] private LosePanel losePanel;

    public void ShowLosePanel()
    {
        losePanel.gameObject.SetActive(true);
    }
}
