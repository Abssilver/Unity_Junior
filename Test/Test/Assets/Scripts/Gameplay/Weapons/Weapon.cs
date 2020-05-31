using System.Collections;
using Gameplay.Weapons.Projectiles;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class Weapon : MonoBehaviour
    {
        //снаряд
        [SerializeField]
        private Projectile _projectile;
        //дуло
        [SerializeField]
        private Transform _barrel;
        //кд
        [SerializeField]
        private float _cooldown;

        //готовность стрелять
        private bool _readyToFire = true;
        //идентификатор оружия
        private UnitBattleIdentity _battleIdentity;
        
        
        //передача идентификатора свой/чужой
        public void Init(UnitBattleIdentity battleIdentity)
        {
            _battleIdentity = battleIdentity;
        }
        
        //произвести выстрел, если не кд. Создать снаряд и предать ему идентификатор и направление, запустить перезарядку
        public void TriggerFire()
        {
            if (!_readyToFire)
                return;
            
            var proj = Instantiate(_projectile, _barrel.position, _barrel.rotation);
            proj.Init(_battleIdentity);
            StartCoroutine(Reload(_cooldown));
        }
        //перезарядка
        private IEnumerator Reload(float cooldown)
        {
            _readyToFire = false;
            yield return new WaitForSeconds(cooldown);
            _readyToFire = true;
        }
        //метод применения ускорения стрельбы
        public void ApplyBonus(float speedUp) => _cooldown = Mathf.Clamp(_cooldown * speedUp, 0.05f, float.MaxValue);

    }
}
