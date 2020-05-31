using UnityEngine;
using Gameplay.ShipSystems;

namespace Gameplay.Bonuses.CustomBonuses
{
    public class WeaponBonus : Bonus
    {
        //значение ускорения оружия, пересчитывается в проценты
        [SerializeField, Range(1f, 99f)]
        private float _weaponSpeedUp;

        //переопределение метода интерфейса для конкретного бонуса
        public override void PickUp(IShipSystem shipSystem) => shipSystem.InteractWithSystem(1 - _weaponSpeedUp * 0.01f);
    }
}
