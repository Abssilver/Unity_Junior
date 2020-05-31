using UnityEngine;
using Gameplay.Weapons;
using Gameplay.GameManagers;

namespace Gameplay.ShipSystems.CustomShipSystems
{
    public class PlayerHealthSystem : HealthSystem, IShipSystem
    {
        //количество жизней игрока
        [SerializeField]
        private int _lifes;
        public float Health => base._health;
        public int Lifes => _lifes;
        //переопределение метода применения урона к игроку
        //значение прочности не может быть меньше 0
        //при достижении очков прочности 0 - отнимаем жизнь и восстанавливаем всю прочность
        //обновляем UI
        public override void ApplyDamage(IDamageDealer damageDealer)
        {
            base._health = Mathf.Clamp(base._health - damageDealer.Damage, 0, base._maxHealth);
            if (base._health <= 0)
            {
                if (_lifes > 0)
                {
                    _lifes--;
                    base._health = base._maxHealth;
                    GameManager.instance.UpdatePlayerHealth(base._health);
                    GameManager.instance.UpdatePlayerLifes(_lifes);
                }
                else
                {
                    GameManager.instance.UpdatePlayerHealth(base._health);
                    Destroy(this.gameObject);
                    GameManager.instance.EndGame();
                }
            }
            else GameManager.instance.UpdatePlayerHealth(base._health);
        }
        //чиним корабль на заданное значение и обновляем UI
        private void RepairShip(float health)
        {
            base._health = Mathf.Clamp(base._health + health, 0, base._maxHealth);
            GameManager.instance.UpdatePlayerHealth(base._health);
        }
        //применяем интерфейс
        public void InteractWithSystem(float health) => RepairShip(health);
    }
}