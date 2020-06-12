using System.Collections.Generic;
using UnityEngine;
using Gameplay.ShipSystems;
using Random = UnityEngine.Random;

namespace Gameplay.Bonuses
{
    public class BonusSpawner : MonoBehaviour
    {
        //перечень выпадающих бонусов
        [SerializeField]
        private List<Bonus> _bonusesForDestroying;
        [SerializeField]
        private HealthSystem _healthSystem;
        //если объект уничтожен потому что кончилось здоровье - создаем бонус
        private void OnDestroy()
        {
            if (_healthSystem.Health <= 0)
                SpawnBonus();
        }
        //создаем бонус на месте уничтоженого объекта
        private void SpawnBonus() =>
            Instantiate(_bonusesForDestroying[Random.Range(0, _bonusesForDestroying.Count)],
            transform.position, transform.rotation);
    }
}