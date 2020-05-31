using Gameplay.ShipSystems;
using UnityEngine;
using Gameplay.Helpers;
using Gameplay.ShipSystems.CustomShipSystems;
using Gameplay.Bonuses;

namespace Gameplay.ShipControllers.CustomControllers
{
    public class PlayerShipController : ShipController, IApplyBonus
    {
        //размер игрока, интересуют свойства extends.x
        [SerializeField]
        private SpriteRenderer _representation;
        //вызываем каждый раз в Update, если нажаты Left/Right arrow или A/D - двигаем корабль 
        //если корабль достигает края экрана, то движение за экран не производим
        protected override void ProcessHandling(MovementSystem movementSystem)
        {
            float movement = Input.GetAxis("Horizontal");
            if (GameAreaHelper
                .IsAvailableForLateralMovement(this.transform, _representation.bounds, movement))
            {
                movementSystem.LateralMovement(movement * Time.deltaTime);
            }
        }
        //вызываем каждый раз в Update, если нажато space - делаем выстрел 
        protected override void ProcessFire(WeaponSystem fireSystem)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                fireSystem.TriggerFire();
            }
        }
        //применяем подобранный бонус
        //в зависимости от типа, передаем одну из подсистем корабля, к которым применяется бонус
        public void ApplyBonus(IPickable bonus)
        {
            switch (bonus.BonusType)
            {
                case BonusType.HealthSystem:
                    if (base.SpaceshipSystems.HealthSystem is PlayerHealthSystem)
                        bonus.PickUp(base.SpaceshipSystems.HealthSystem as PlayerHealthSystem);
                    break;
                case BonusType.WeaponSystem:
                    if (base.SpaceshipSystems.WeaponSystem is PlayerWeaponSystem)
                        bonus.PickUp(base.SpaceshipSystems.WeaponSystem as PlayerWeaponSystem);
                    break;
                default:
                    break;
            }
        }
    }
}
