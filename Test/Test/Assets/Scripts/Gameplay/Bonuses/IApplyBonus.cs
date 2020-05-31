namespace Gameplay.Bonuses
{
    //интерфейс, показывающий, что объект может применять бонусы
    public interface IApplyBonus
    {
        void ApplyBonus(IPickable bonus);
    }
}
