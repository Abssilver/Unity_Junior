using Gameplay.ShipSystems;
using UnityEngine;

namespace Gameplay.ShipControllers.CustomControllers
{
    sealed class EnemyComplexShipController : EnemyShipController
    {
        //амплитуда движения по синусоиде
        [SerializeField, Range(1f, 2f)]
        float _rangeOfMotion;
        //добавление движения корабля по синусоиде влево-вправо
        protected override void ProcessHandling(MovementSystem movementSystem)
        {
            base.ProcessHandling(movementSystem);
            float lateralMove = Mathf.Sin(Time.time);
            movementSystem.LateralMovement(lateralMove * 0.001f * _rangeOfMotion);
        }
    }
}