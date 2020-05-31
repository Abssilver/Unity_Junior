using Gameplay.ShipSystems;
using Gameplay.Spaceships;
using UnityEngine;

namespace Gameplay.ShipControllers
{
    public abstract class ShipController : MonoBehaviour
    {
        //была произведена замена внутренного поля на свойство
        protected ISpaceship SpaceshipSystems { get; private set; }
        public void Init(ISpaceship spaceship)
        {
            SpaceshipSystems = spaceship;
        }

        //вызов проверки на движение/атаку
        private void Update()
        {
            ProcessHandling(SpaceshipSystems.MovementSystem);
            ProcessFire(SpaceshipSystems.WeaponSystem);
        }

        protected abstract void ProcessHandling(MovementSystem movementSystem);
        protected abstract void ProcessFire(WeaponSystem fireSystem);
    }
}
