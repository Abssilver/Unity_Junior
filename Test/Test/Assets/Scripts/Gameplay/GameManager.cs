using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using Gameplay.Spawners;

namespace Gameplay.GameManagers
{
    public class GameManager : MonoBehaviour
    {
        //инстанс GameManager-а
        public static GameManager instance;
        //количество набранных очков
        private int _score = 0;
        //количество жизней игрока
        [SerializeField]
        private int _playerLifes;
        //делегаты для вызова обновлений UI
        private Action<int> updateScore;
        private Action<float> updateHealth;
        private Action<int> updateLifes;
        private Action endGame;
        //ссылка на игрока для перезагрузки
        [SerializeField]
        private GameObject _player;
        //место создания игрока
        [SerializeField]
        private Transform _playerSpawnPoint;
        //перечень мест создания вражеских кораблей
        [SerializeField]
        private List<Spawner> _spawners;
        //можно action updateScore подцепить к свойству, 
        //чтобы при изменении значения очков, делегат автоматически вызывался
        public int Score
        {
            private set
            {
                _score = value;
                updateScore?.Invoke(_score);
            }
            get => _score;
        }
        //то же самое делаем с жизнями
        public int PlayerLifes
        {
            private set 
            {
                _playerLifes = value;
                updateLifes?.Invoke(_playerLifes);
            }
            get => _playerLifes;
        }
        //создаем ссылку на инстанс
        private void Awake()
        {
            if (instance != null && instance != this)
                Destroy(this.gameObject);
            else
                instance = this;
        }
        //подписки на делегаты
        public void SubscribeToScoreAction(Action<int> subscriber)
        {
            if (updateScore == null)
                updateScore += subscriber;
        }
        public void SubscribeToHealthAction(Action<float> subscriber)
        {
            if (updateHealth == null)
                updateHealth += subscriber;
        }
        public void SubscribeToLifeAction(Action<int> subscriber)
        {
            if (updateLifes == null)
                updateLifes += subscriber;
        }
        public void SubscribeToEndGame(Action subscriber) => endGame += subscriber;

        //Реакция на уничтожение игрока
        public void PlayerDestroyed()
        {
            if (PlayerLifes > 0)
            {
                PlayerLifes--;
                StartCoroutine(SpawnPlayer());
            }
            else
                EndGame();
        }
        //вызов делегатов
        public void UpdatePlayerHealth(float health) => updateHealth?.Invoke(health);
        //public void UpdatePlayerLifes(int life) => updateLifes?.Invoke(life);
        //public void UpdatePlayerScore() => updateScore?.Invoke(_score);
        private void EndGame()
        {
            _spawners.ForEach(spawner => spawner.StopSpawn());
            endGame?.Invoke();
        }
        //вызов обновления UI и создание игрока
        private void Start()
        {
            Score = _score;
            PlayerLifes = _playerLifes;
            StartCoroutine(SpawnPlayer());
        }
        public void AddScore(int valueToAdd) => Score += valueToAdd;

        //public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);

        //метод сбрасывающий очки, создающий нового игрока и запускающий создание новых кораблей противников
        public void RestartGame()
        {
            Score = 0;
            PlayerLifes = 1;
            _spawners.ForEach(spawner => spawner.StartSpawn());
            StartCoroutine(SpawnPlayer());
        }
        //запуск создания игрока. Необходима задержка, иначе возникает nullreference exception у MovementSystem
        private IEnumerator SpawnPlayer()
        {
            yield return new WaitForSeconds(0.5f);
            var newPlayer = Instantiate(_player, _playerSpawnPoint.parent);
            newPlayer.transform.position = _playerSpawnPoint.position;
            newPlayer.transform.rotation = _playerSpawnPoint.rotation;
        }
    }
}
