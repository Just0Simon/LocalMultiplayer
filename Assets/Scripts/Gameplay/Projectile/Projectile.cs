using Unity.Netcode;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    [field: SerializeField]
    public int Damage { get; private set; }
    
    [SerializeField]
    private float _speed = 5f;

    private Vector3 _translation;
    
    private void Update()
    {
        transform.Translate(_translation, Space.World);
    }
    
    private void OnCollisionEnter(Collision _)
    {
        if(!IsHost)
            return;
        
        Destroy(gameObject);
    }
    
    public void Launch(Vector3 direction)
    {
        _translation = direction * _speed * Time.deltaTime;
    }
}