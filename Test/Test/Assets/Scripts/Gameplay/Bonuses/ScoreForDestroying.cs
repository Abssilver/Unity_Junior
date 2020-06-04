using UnityEngine;
using Gameplay.ShipSystems;

namespace Gameplay.GameManagers
{
    public class ScoreForDestroying : MonoBehaviour
    {
        //количество очков за уничтожение данного объекта
        [SerializeField]
        private int _scoreForDestroying;
        [SerializeField]
        private HealthSystem _healthSystem;
        //если объект уничтожен потому что кончилось здоровье - добавляем очки
        private void OnDestroy()
        {
            if (_healthSystem.Health <= 0)
                AddScore();
        }
        //обращение к менеджеру и вызов метода добавления очков
        private void AddScore() => GameManager.instance.AddScore(_scoreForDestroying);
    }
}
