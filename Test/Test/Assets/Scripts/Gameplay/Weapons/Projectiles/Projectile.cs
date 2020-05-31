using UnityEngine;

namespace Gameplay.Weapons.Projectiles
{
    public abstract class Projectile : MonoBehaviour, IDamageDealer
    {

        [SerializeField]
        private float _speed;

        [SerializeField] 
        private float _damage;

        //можно использовать автосвойство
        private UnitBattleIdentity _battleIdentity;

        public UnitBattleIdentity BattleIdentity => _battleIdentity;
        public float Damage => _damage;


        //передача идентификатора свой/чужой
        public void Init(UnitBattleIdentity battleIdentity)
        {
            _battleIdentity = battleIdentity;
        }
        
        //движение снаряда
        private void Update()
        {
            Move(_speed);
        }

        //если снаряд коснулся чего-то, то проверяем, можно ли у него отнять здоровье,
        //если можно, то проверяем на свой/чужой и применяем нанесение урона
        private void OnCollisionEnter2D(Collision2D other)
        {
            var damagableObject = other.gameObject.GetComponent<IDamagable>();
            
            if (damagableObject != null 
                && damagableObject.BattleIdentity != BattleIdentity)
            {
                damagableObject.ApplyDamage(this);
                Destroy(this.gameObject);
            }
        }
        
        protected abstract void Move(float speed);
    }
}
