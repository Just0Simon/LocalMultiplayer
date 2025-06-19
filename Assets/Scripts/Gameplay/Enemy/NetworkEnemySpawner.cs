using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class NetworkEnemySpawner : NetworkBehaviour
    {
        [SerializeField]
        private float _delay = 5f;
        [SerializeField]
        private NetworkObject _enemyNetworkObjectPrefab;
        
        [SerializeField]
        private List<Transform> _spawnPoints;
        
        private IEnemiesFactory _enemiesFactory;

        private Enemy _lastSpawnedEnemy;
        
        private void Awake()
        {
            _enemiesFactory = new NetworkedEnemiesFactory(_enemyNetworkObjectPrefab);
        }

        private void Start()
        {
            if(IsHost)
                StartCoroutine(SpawnEnemyRoutine());
        }

        private IEnumerator SpawnEnemyRoutine()
        {
            yield return new WaitForSeconds(_delay);
            
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            var randomSpawnPointIndex = Random.Range(0, _spawnPoints.Count);
            var spawnPoint = _spawnPoints[randomSpawnPointIndex];
            var enemy = _enemiesFactory.Create(spawnPoint.position);
            _lastSpawnedEnemy = enemy;
        }

        private void OnTriggerExit(Collider other)
        {
            if(!IsHost)
                return;
            
            var leavedEnemy = other.GetComponent<Enemy>();
            if(leavedEnemy is null)
                return;

            if (leavedEnemy == _lastSpawnedEnemy)
            {
                StartCoroutine(SpawnEnemyRoutine());
            }
        }
    }
}