using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Spawners
{
    public class Spawner : MonoBehaviour
    {
        //перечень врагов
        [SerializeField]
        private List<GameObject> _objects;
        
        [SerializeField]
        private Transform _parent;
        
        [SerializeField]    
        private Vector2 _spawnPeriodRange;
        
        [SerializeField]
        private Vector2 _spawnDelayRange;

        [SerializeField]
        private bool _autoStart = true;


        private void Start()
        {
            if (_autoStart)
                StartSpawn();
        }
        public void StartSpawn()
        {
            StartCoroutine(Spawn());
        }
        public void StopSpawn()
        {
            StopAllCoroutines();
        }
        private IEnumerator Spawn()
        {
            yield return new WaitForSeconds(Random.Range(_spawnDelayRange.x, _spawnDelayRange.y));
            
            while (true)
            {
                if (_objects!=null && _objects.Count > 0)
                    Instantiate(_objects[Random.Range(0, _objects.Count)], transform.position, transform.rotation, _parent);
                yield return new WaitForSeconds(Random.Range(_spawnPeriodRange.x, _spawnPeriodRange.y));
            }
        }
    }
}
