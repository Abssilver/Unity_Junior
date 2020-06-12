using UnityEngine;
using Gameplay.Weapons;

namespace Gameplay.ShipSystems
{
    public class HealthSystem : MonoBehaviour
    {
        //текущее значение прочности
        [SerializeField]
        protected float _health;
        //максимальное значение прочности
        [SerializeField]
        protected float _maxHealth;
        public virtual float Health
        {
            protected set => _health = value;
            get => _health;
        }
        //применение урона к объекту при взаимодествие с вражеским снарядом
        //Если объект потерял всю прочность, то уничтожаем его
        public virtual void ApplyDamage(IDamageDealer damageDealer)
        {
            Health = Mathf.Clamp(Health - damageDealer.Damage, 0, _maxHealth);
            //_health -= damageDealer.Damage;
            if (Health <= 0)
                Destroy(this.gameObject);
        }
    }
}
