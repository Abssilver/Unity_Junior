using System.Collections;
using Gameplay.ShipSystems;
using UnityEngine;

namespace Gameplay.ShipControllers.CustomControllers
{
    public class EnemyShipController : ShipController
    {
        //задержка выстрела для ботов
        [SerializeField]
        private Vector2 _fireDelay;

        private bool _fire = true;
        //движение врага по прямой (по направлению корабля)
        protected override void ProcessHandling(MovementSystem movementSystem)
        {
            movementSystem.LongitudinalMovement(Time.deltaTime);
        }
        //если можно стрелять - стреляем и идем на кд со случайной задержкой
        protected override void ProcessFire(WeaponSystem fireSystem)
        {
            if (!_fire)
                return;

            fireSystem.TriggerFire();
            StartCoroutine(FireDelay(Random.Range(_fireDelay.x, _fireDelay.y)));
        }
        private IEnumerator FireDelay(float delay)
        {
            _fire = false;
            yield return new WaitForSeconds(delay);
            _fire = true;
        }
    }
}
