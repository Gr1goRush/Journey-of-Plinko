using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Game.UI
{
    public class LosePanel : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI balanceText;

        private void OnEnable()
        {
            balanceText.text = GameController.Instance.Balance.ToString();
        }


        public void LoadMenu()
        {
            Time.timeScale = 1f;
            ScenesLoader.LoadMenu();
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            ScenesLoader.LoadGame();
        }
    }
}