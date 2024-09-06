using UnityEngine;

public class GameController : Singleton<GameController>
{
    public int Balance => balance;

    [SerializeField] private int startBalance = 1000, balanceToWin = 2000; // betAmount = 100;

    [SerializeField] private Playground playground;

    private int balance = 1000;//winAmount = 0, 

    void Start()
    {
        LevelConfiguration levelConfiguration = LevelsManager.Instance.LoadSelectedLevelContent();

        playground.ObstaclesGrid.Initialize(levelConfiguration.obstaclesRows, levelConfiguration.obstacleBlocks);

        playground.Ball.Init();

        balance = 0;
        SetWinProgress();

        //SetWinAmount(winAmount);
        balance = startBalance;
        SetBalance(balance);

        for (int i = 0; i < levelConfiguration.targetMarks.Length; i++)
        {
            playground.SetTargetMark(i, levelConfiguration.targetMarks[i]);
        }
    }

    public void Twist()
    {
        //if(!BalanceManager.Instance.HasBalance(betAmount))
        //{
        //    return;
        //}

        //BalanceManager.Instance.SubtractBalance(betAmount);

        GameSoundsController.Instance.PlayOneShot("twist");

        Ball ball = playground.Ball.Pull();
        ball.transform.localPosition = Vector3.zero;
    }

    public void MultipleBalance(float multiplier)
    {
        SetBalance(Mathf.RoundToInt(balance * multiplier));
        SetWinProgress();

        if(balance <= 0)
        {
            Lose();
        }
    }

    private void SetWinProgress()
    {
        UIController.Instance.MainPanel.SetWinProgress(balance, balanceToWin);

        if(balance >= balanceToWin)
        {
            BalanceManager.Instance.AddBalance(balance);

            ScenesLoader.LoadMenu();
        }
    }

    public void Lose()
    {
        Time.timeScale = 0f;
        UIController.Instance.ShowLosePanel();
    }

    private void SetBalance(int amount)
    {
        balance = amount;
        UIController.Instance.MainPanel.SetBalance(balance);
    }

    public void StartSuperGame()
    {
        Time.timeScale = 0f;
        UIController.Instance.SuperGamePanel.gameObject.SetActive(true);
    }

    public void CancelSuperGame()
    {
        Time.timeScale = 1f;
    }

    public void CompleteSuperGame(SuperGameWin winType)
    {
        Time.timeScale = 1f;

        if(winType == SuperGameWin.AddCoins)
        {
            SetBalance(balance + 100);
            SetWinProgress();
        }
        else if (winType == SuperGameWin.Fail)
        {
            Lose();
        }
        else
        {
            LevelsManager.Instance.UnlockRandomLevel();
        }
    }
}
