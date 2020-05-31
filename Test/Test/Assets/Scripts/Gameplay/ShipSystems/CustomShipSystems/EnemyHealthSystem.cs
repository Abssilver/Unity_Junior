using System.Collections.Generic;
using UnityEngine;
using Gameplay.Weapons;
using Random = UnityEngine.Random;
using Gameplay.Bonuses;
using Gameplay.GameManagers;

namespace Gameplay.ShipSystems.CustomShipSystems
{
    public class EnemyHealthSystem : HealthSystem
    {
        //количество очков за уничтожение данного объекта
        [SerializeField]
        private int _scoreForDestroying;
        //перечень выпадающих бонусов
        [SerializeField]
        private List<Bonus> _bonusesForDestroying;
        //переопределение метода применения урона.
        //Если объект потерял всю прочность, то добавляем очки и создаем бонус
        public override void ApplyDamage(IDamageDealer damageDealer)
        {
            base._health -= damageDealer.Damage;
            if (base._health <= 0)
            {
                AddScore();
                SpawnBonus();
                Destroy(this.gameObject);
            }
        }
        //обращение к менеджеру и вызов метода добавления очков
        private void AddScore() => GameManager.instance.AddScore(_scoreForDestroying);
        //создаем бонус на месте уничтоженого объекта
        private void SpawnBonus() =>
            Instantiate(_bonusesForDestroying[Random.Range(0, _bonusesForDestroying.Count)],
            transform.position, transform.rotation);
    }
}
