using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winAmountText, balanceAmountText;
    
    public void SetWinProgress(int amount, int condition)
    {
        winAmountText.text = amount + "/" + condition;
    }

    public void SetBalance(int amount)
    {
        balanceAmountText.text = amount.ToString();
    }

    public void ToMenu()
    {
        Time.timeScale = 1f;
        ScenesLoader.LoadMenu();
    }
}
