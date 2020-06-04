using Gameplay.ShipSystems;
namespace Gameplay.Bonuses
{
    //интерфейс, показывающий, что объект можно поднять
    public interface IPickable<T> where T : IShipSystem
    {
        void PickUp(IShipSystem shipsystem);
    }
}
