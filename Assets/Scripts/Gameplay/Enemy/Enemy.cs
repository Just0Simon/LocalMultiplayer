using Gameplay;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : NetworkBehaviour
{
    [SerializeField]
    private float _updateTargetPositionDelay = 0.2f;
    
    [SerializeField]
    private PlayerDetector _playerDetector;
    [SerializeField]
    private EnemyShootComponent _enemyShootComponent;
    
    [SerializeField]
    private NavMeshAgent _agent;

    private float _updateTargetPositionTimer = 0f;
    private Transform _currentTarget;
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        _agent.updatePosition = false;
        
        _playerDetector.ClosestPlayerChanged += OnClosestPlayerChanged;
    }
    
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        _playerDetector.ClosestPlayerChanged -= OnClosestPlayerChanged;
    }

    private void OnClosestPlayerChanged(Player player)
    {
        SetTarget(player.transform);
    }

    private void SetTarget(Transform targetTransform)
    {
        _currentTarget = targetTransform;
        
        _agent.SetDestination(targetTransform.position);
        
        _enemyShootComponent.SetTarget(_currentTarget);
    }

    private void Update()
    {
        transform.Translate(_agent.velocity * Time.deltaTime, Space.World);

        UpdateTargetPositionTimer();
    }

    private void UpdateTargetPositionTimer()
    {
        if(_currentTarget is null)
            return;
        
        _updateTargetPositionTimer += Time.deltaTime;
        if (_updateTargetPositionTimer >= _updateTargetPositionDelay)
        {
            _agent.SetDestination(_currentTarget.position);
        }
    }
}
