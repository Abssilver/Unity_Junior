using UnityEngine;
using Gameplay.Weapons;

namespace Gameplay.ShipSystems
{
    public abstract class HealthSystem : MonoBehaviour
    {
        //текущее значение прочности
        [SerializeField]
        private protected float _health;
        //максимальное значение прочности
        [SerializeField]
        private protected float _maxHealth;
        //применение урона к объекту при взаимодествие с вражеским снарядом
        public abstract void ApplyDamage(IDamageDealer damageDealer);
    }
}
