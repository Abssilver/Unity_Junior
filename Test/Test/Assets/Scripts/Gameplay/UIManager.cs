using UnityEngine;
using UnityEngine.UI;
using Gameplay.Spaceships;
using Gameplay.ShipSystems.CustomShipSystems;

namespace Gameplay.GameManagers
{
    public class UIManager : MonoBehaviour
    {
        //ссылки на UI
        [SerializeField]
        private Text _scoreValue;
        [SerializeField]
        private Text _healthValue;
        [SerializeField]
        private Text _lifeValue;
        [SerializeField]
        private Text _resultsValue;
        [SerializeField]
        private RectTransform _endGameWindow;
        [SerializeField]
        private Button _restartButton;

        //Подписка на делегаты GameManager-a и инициализация начальных значений UI
        //Добавление к кнопке рестарт методов (делегатов)
        private void Start()
        {
            GameManager.instance.SubscribeToScoreAction(UpdateScore);
            GameManager.instance.SubscribeToHealthAction(UpdateHealth);
            GameManager.instance.SubscribeToLifeAction(UpdateLife);

            InitPlayerStats();

            GameManager.instance.SubscribeToEndGame(UpdateFinalResults);
            GameManager.instance.SubscribeToEndGame(DisplayEndGameWindow);

            _restartButton.onClick.AddListener(GameManager.instance.RestartGame);
            _restartButton.onClick.AddListener(DisableEndGamePanel);
            _restartButton.onClick.AddListener(InitPlayerStats);
        }
        //Методы обновления UI
        private void UpdateScore(int score) => _scoreValue.text = score.ToString();
        private void UpdateHealth(float remainHealth) => _healthValue.text = remainHealth.ToString();
        private void UpdateLife(int remainLife) => _lifeValue.text = remainLife.ToString();
        private void UpdateFinalResults() => _resultsValue.text = GameManager.instance.Score.ToString();
        
        //Метод инициализации UI значений игрока (жизни, прочность), а также очков
        private void InitPlayerStats()
        {
            PlayerHealthSystem player = GameObject.FindGameObjectWithTag("Player")
                .GetComponent<Spaceship>()
                .HealthSystem as PlayerHealthSystem;
            if (player != null)
            {
                GameManager.instance.UpdatePlayerHealth(player.Health);
                GameManager.instance.UpdatePlayerLifes(player.Lifes);
                GameManager.instance.UpdatePlayerScore();
            }
        }
        //Включение и выключение панели
        private void DisplayEndGameWindow() => _endGameWindow.gameObject.SetActive(true);
        private void DisableEndGamePanel() => _endGameWindow.gameObject.SetActive(false);
    }
}
