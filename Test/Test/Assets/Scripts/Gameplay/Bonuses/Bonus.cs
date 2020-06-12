using UnityEngine;
using Gameplay.ShipSystems;

namespace Gameplay.Bonuses
{
    public abstract class Bonus : MonoBehaviour, IPickable<IShipSystem>
    {
        //скорость полета бонуса
        [SerializeField]
        private float _speed;

        //метод интерфейса. Вызывает действие бонуса
        public abstract void PickUp(IShipSystem shipSystem);
        private void Update()
        {
            Move(_speed);
        }
        //При соприкосновении проверяем, можно ли применить бонус
        //Если можно применяем, а объект уничтожаем
        private void OnCollisionEnter2D(Collision2D other)
        {
            var applyBonusInterface = other.gameObject.GetComponent<IApplyBonus>();
            if (applyBonusInterface != null)
            {
                applyBonusInterface.ApplyBonus(this);
                Destroy(this.gameObject);
            }
        }
        //двигаем объект
        protected virtual void Move(float speed)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.up);
        }
    }
}
