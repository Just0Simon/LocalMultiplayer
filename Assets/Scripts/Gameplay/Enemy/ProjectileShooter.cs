using Unity.Netcode;
using UnityEngine;

public class ProjectileShooter : NetworkBehaviour
{
    [SerializeField]
    public float _delay = 5f;
    
    [SerializeField]
    private Projectile _projectilePrefab;
    
}