using Unity.Netcode;
using UnityEngine;

public class EnemyShootComponent : NetworkBehaviour
{
    [SerializeField]
    private float _delay = 3f;
    
    [SerializeField]
    private float _projectileSpawnDistance;
    
    [SerializeField]
    private NetworkObject _projectileNetworkObjectPrefab;

    private float _timer;
    private Transform _target;
    private IProjectileFactory _projectileFactory;

    private void Awake()
    {
        _projectileFactory = new NetworkedProjectileFactory(_projectileNetworkObjectPrefab);
    }

    public void SetTarget(Transform targetTransform)
    {
        _target = targetTransform;   
    }

    public void Update()
    {
        ShootTick();
    }

    private void ShootTick()
    {
        if(_target is null)
            return;
        
        _timer += Time.deltaTime;

        if (_timer >= _delay)
        {
            _timer = 0;
            ShootProjective();
        }
    }

    private void ShootProjective()
    {
        if (!IsHost)
            return;
        
        var projectile = _projectileFactory.Create(transform.position);
        
        var direction = (_target.position - transform.position).normalized;
        direction.y = 0;
        
        projectile.Launch(direction);
    }
}