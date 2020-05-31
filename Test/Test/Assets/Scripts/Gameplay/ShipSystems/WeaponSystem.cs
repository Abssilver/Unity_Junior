using System.Collections.Generic;
using Gameplay.Weapons;
using UnityEngine;
using System.Linq;

namespace Gameplay.ShipSystems
{
    public class WeaponSystem : MonoBehaviour
    {
        [SerializeField]
        private List<Weapon> _weapons;
        protected List<Weapon> Weapons => _weapons;

        //передача идентификатора каждому оружию
        public void Init(UnitBattleIdentity battleIdentity)
        {
            _weapons.ForEach(w => w.Init(battleIdentity));
        }
        
        //при нажании срабатывании триггера (напр.клавиши пробел) происходит выстрел
        public void TriggerFire()
        {
            _weapons.ForEach(w => w.TriggerFire());
        }

    }
}
