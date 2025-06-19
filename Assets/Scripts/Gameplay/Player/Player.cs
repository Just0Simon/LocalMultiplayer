using Gameplay.Components;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField]
    private HealthComponent _healthComponent;
    [SerializeField]
    private PlayerMovementComponent playerMovementComponent;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        _healthComponent.HealthChanged += OnHealthChanged;
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        _healthComponent.HealthChanged -= OnHealthChanged;
        
        NetworkManager.Singleton.Shutdown();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (NetworkManager.Singleton.IsHost)
        {
            var projectile = other.gameObject.GetComponent<Projectile>();
            if (projectile != null)
            {
                _healthComponent.TakeDamage(projectile.Damage);
            }
        }
    }
    
    private void OnHealthChanged(int newHealth)
    {
        if (!NetworkManager.Singleton.IsHost)
            return;
        
        if(newHealth <= 0)
            Destroy(gameObject);
    }
}
