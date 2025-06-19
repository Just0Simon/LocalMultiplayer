using Unity.Netcode;
using UnityEngine;

public class NetworkedProjectileFactory : IProjectileFactory
{
    private readonly NetworkObject _projectileNetworkObjectPrefab;

    public NetworkedProjectileFactory(NetworkObject projectileNetworkObjectPrefab)
    {
        _projectileNetworkObjectPrefab = projectileNetworkObjectPrefab;
    }

    public Projectile Create(Vector3 position)
    {
        var enemyNetworkObject = NetworkManager.Singleton.SpawnManager.InstantiateAndSpawn(_projectileNetworkObjectPrefab, 0UL, true, false,false, position, Quaternion.identity);

        var projectile = enemyNetworkObject.GetComponent<Projectile>();

        return projectile;
    }
        
    public Projectile Create(Vector3 position, Quaternion rotation)
    {
        var enemyNetworkObject = NetworkManager.Singleton.SpawnManager.InstantiateAndSpawn(_projectileNetworkObjectPrefab, 0UL, true, false,false, position, rotation);

        var projectile = enemyNetworkObject.GetComponent<Projectile>();

        return projectile;
    }
}