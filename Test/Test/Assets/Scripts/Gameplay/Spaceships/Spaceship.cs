using Gameplay.ShipControllers;
using Gameplay.ShipSystems;
using Gameplay.Weapons;
using UnityEngine;

namespace Gameplay.Spaceships
{
    public class Spaceship : MonoBehaviour, ISpaceship, IDamagable
    {
        //содержит в себе инициализацию интерфейса системы движения/атаки, а также update для этих систем
        //Достаем из PlayerShipController на игроке
        [SerializeField]
        private ShipController _shipController;
        //ссылка на управление движением
        //Достаем из MovementSystem на игроке
        [SerializeField]
        private MovementSystem _movementSystem;
        //ссылка на систему атаки
        //Достаем из WeaponSystem на игроке
        [SerializeField]
        private WeaponSystem _weaponSystem;
        //ссылка на систему жизней
        //Достаем из HealthSystem на игроке
        [SerializeField]
        private HealthSystem _healthSystem;
        //идентификатор объекта на поле
        [SerializeField]
        private UnitBattleIdentity _battleIdentity;

        //свойства на чтение
        public MovementSystem MovementSystem => _movementSystem;
        public WeaponSystem WeaponSystem => _weaponSystem;
        public HealthSystem HealthSystem => _healthSystem;
        public UnitBattleIdentity BattleIdentity => _battleIdentity;

        //передача ссылки на систему движения/атаки
        //передача идентификатора объекта к системе вооружения -> оружию
        private void Start()
        {
            _shipController.Init(this);
            _weaponSystem.Init(_battleIdentity);
        }
        //метод применения урона к кораблю
        public void ApplyDamage(IDamageDealer damageDealer)
        {
            _healthSystem.ApplyDamage(damageDealer);
            //Destroy(gameObject);
        }

    }
}
