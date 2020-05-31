namespace Gameplay.ShipSystems.CustomShipSystems
{
    public class PlayerWeaponSystem : WeaponSystem, IShipSystem
    {
        //применяем интефейс
        public void InteractWithSystem(float speedUp) => base.Weapons.ForEach(w => w.ApplyBonus(speedUp));
    }
}