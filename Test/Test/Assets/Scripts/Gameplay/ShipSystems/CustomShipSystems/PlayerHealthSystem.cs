using UnityEngine;
using Gameplay.GameManagers;

namespace Gameplay.ShipSystems.CustomShipSystems
{
    public class PlayerHealthSystem : HealthSystem, IShipSystem
    {
        //Переопределение свойства, при измененнии значения происходит обновление UI через GameManager
        public override float Health
        {
            protected set 
            {
                base._health = value;
                GameManager.instance.UpdatePlayerHealth(base._health);
            }
            get => base._health;
        }
        //GameManager не успевает за изменением. Может попробовать перенести создание инстанса на этап раньше. OnEnable
        //Потому и используется создание игрока
        private void Start()
        {
            this.Health = base._health;
        }
        
        //Сообщаем менеджеру, что игрок уничтожен
        private void OnDestroy() => GameManager.instance.PlayerDestroyed();

        //чиним корабль на заданное значение и обновляем UI
        private void RepairShip(float health) => Health = Mathf.Clamp(Health + health, 0, base._maxHealth);
        //применяем интерфейс
        public void InteractWithSystem(float health) => RepairShip(health);
    }
}