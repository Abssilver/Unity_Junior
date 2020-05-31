using Gameplay.ShipSystems;
namespace Gameplay.Bonuses
{
    //интерфейс, показывающий, что объект можно поднять
    public interface IPickable
    {
        BonusType BonusType { get; }
        void PickUp(IShipSystem shipsystem);
    }
    //перечисление, указывающее на тип бонуса
    public enum BonusType
    {
        HealthSystem,
        WeaponSystem
    }
}
