using System.Collections.Generic;
using UnityEngine;
using System;
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
        //можно action upadateScore подцепить к свойству, 
        //чтобы при изменении значения очков, делегат автоматически вызывался
        public int Score => _score;
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
        //вызов делегатов
        public void UpdatePlayerHealth(float health) => updateHealth?.Invoke(health);
        public void UpdatePlayerLifes(int life) => updateLifes?.Invoke(life);
        public void UpdatePlayerScore() => updateScore?.Invoke(_score);
        public void EndGame()
        {
            _spawners.ForEach(spawner => spawner.StopSpawn());
            endGame?.Invoke();
        }
        //добавление очков и вызов обновления UI
        public void AddScore(int valueToAdd)
        {
            _score += valueToAdd;
            UpdatePlayerScore();
        }
        //public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        
        //метод сбрасывающий очки, создающий нового игрока и запускающий создание новых кораблей противников
        public void RestartGame()
        {
            _score = 0;
            _spawners.ForEach(spawner => spawner.StartSpawn());
            GameObject newPlayer = Instantiate(_player, _playerSpawnPoint.parent) as GameObject;
            newPlayer.transform.position = _playerSpawnPoint.position;
            newPlayer.transform.rotation = _playerSpawnPoint.rotation;
        }
    }
}
