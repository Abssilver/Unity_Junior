using Gameplay.ShipSystems;

namespace Gameplay.Spaceships
{
    public interface ISpaceship
    {
        HealthSystem HealthSystem { get; }
        MovementSystem MovementSystem { get; }
        WeaponSystem WeaponSystem { get; }

    }
}
