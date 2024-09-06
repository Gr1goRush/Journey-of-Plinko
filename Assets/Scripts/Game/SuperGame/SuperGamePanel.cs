using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SuperGameWin
{
    AddCoins, Fail, UnlockLevel
}

[System.Serializable]
public struct SuperGameCardConfiguration
{
    public AnimatorOverrideController animatorController;
    public SuperGameWin winType;

}

public class SuperGamePanel : MonoBehaviour
{
    [SerializeField] private GameObject cardsPanel, confirmPanel;
    [SerializeField] private Sprite hiddenSprite;

    [SerializeField] private SuperGameCardConfiguration[] cardsConfigurations;
    [SerializeField] private SuperGameCard[] cardsObjects;

    private int selectingCardIndex = -1;

    private int[] selectedCardIndexes = null;

    private const int cardsSelectCount = 2;

    void Start()
    {
        for (int i = 0; i < cardsObjects.Length; i++)
        {
            int index = i;
            cardsObjects[i].AddClickListener(() => SelectCard(index));
        }
    }

    private void OnEnable()
    {
        cardsPanel.SetActive(false);
        confirmPanel.SetActive(true);
    }

    public void ConfirmNo()
    {
        gameObject.SetActive(false);

        GameController.Instance.CancelSuperGame();
    }

    public void ConfirmYes()
    {
        cardsPanel.SetActive(true);
        confirmPanel.SetActive(false);

        for (int i = 0; i < cardsObjects.Length; i++)
        {
            cardsObjects[i].ShowDefaultAnimation();
            cardsObjects[i].SetInteractable(true);
            cardsObjects[i].SetAddBalanceFrameActive(false);
        }

        selectedCardIndexes = new int[cardsSelectCount];
        for (int i = 0; i < selectedCardIndexes.Length; i++)
        {
            selectedCardIndexes[i] = -1;
        }

        selectingCardIndex = 0;
    }

    private void SelectCard(int index)
    {
        int cardIndex = Random.Range(0, cardsConfigurations.Length);
     //   cardIndex = 1;

        selectedCardIndexes[selectingCardIndex] = cardIndex;

        cardsObjects[index].SetInteractable(false);

        cardsObjects[index].ShowOpenAnimation(cardsConfigurations[cardIndex].animatorController);
        cardsObjects[index].SetAddBalanceFrameActive(false);

        selectingCardIndex++;

        if (selectingCardIndex >= cardsSelectCount)
        {
            for (int i = 0; i < cardsObjects.Length; i++)
            {
                cardsObjects[i].SetInteractable(false);
            }

            this.Invoke(OnSelectAnimationShowed, 1.5f);
        }
    }

    private void OnSelectAnimationShowed()
    {
        if (selectedCardIndexes[0] == selectedCardIndexes[1])
        {

            SuperGameWin winType = cardsConfigurations[selectedCardIndexes[0]].winType;
            gameObject.SetActive(false);
            GameController.Instance.CompleteSuperGame(winType);
        }
        else
        {
            Time.timeScale = 1f;

            gameObject.SetActive(false);
        }
    }
}
