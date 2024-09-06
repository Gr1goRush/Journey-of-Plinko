using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelsPanelButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private GameObject coinsPanel, playPanel;

    public void SetPrice(int price)
    {
        priceText.text = price.ToString();
        coinsPanel.SetActive(true);
        playPanel.SetActive(false);
    }

    public void SetUnlocked()
    {
        coinsPanel.SetActive(false);
        playPanel.SetActive(true);
    }

    public void AddClickListener(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }
}
