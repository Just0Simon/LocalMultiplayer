using Unity.Netcode;
using UnityEngine;

public class NetworkedEnemiesFactory : IEnemiesFactory
{
    private readonly NetworkObject _enemyNetworkObjectPrefab;

    public NetworkedEnemiesFactory(NetworkObject enemyNetworkObjectPrefab)
    {
        _enemyNetworkObjectPrefab = enemyNetworkObjectPrefab;
    }

    public Enemy Create(Vector3 position)
    {
        var enemyNetworkObject = NetworkManager.Singleton.SpawnManager.InstantiateAndSpawn(_enemyNetworkObjectPrefab, 0UL, true, false,false, position, Quaternion.identity);

        var enemy = enemyNetworkObject.GetComponent<Enemy>();

        return enemy;
    }
        
    public Enemy Create(Vector3 position, Quaternion rotation)
    {
        var enemyNetworkObject = NetworkManager.Singleton.SpawnManager.InstantiateAndSpawn(_enemyNetworkObjectPrefab, 0UL, true, false,false, position, rotation);

        var enemy = enemyNetworkObject.GetComponent<Enemy>();

        return enemy;
    }
}