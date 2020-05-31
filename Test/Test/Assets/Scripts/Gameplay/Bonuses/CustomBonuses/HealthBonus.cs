using UnityEngine;
using Gameplay.ShipSystems;

namespace Gameplay.Bonuses.CustomBonuses
{
    public class HealthBonus : Bonus
    {
        //значение на которое чиним корабль
        [SerializeField]
        private float _healthBonusValue;

        //переопределяем метод интефейса для конкретного бонуса
        public override void PickUp(IShipSystem shipSystem) => shipSystem.InteractWithSystem(_healthBonusValue);
    }
}
