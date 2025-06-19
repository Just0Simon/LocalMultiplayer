using Unity.Netcode;
using UnityEngine;

public class LocalPlayerCamera : NetworkBehaviour
{
    [SerializeField]
    private Camera _camera;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        
        _camera.gameObject.SetActive(IsLocalPlayer);
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkSpawn();
        
        _camera.gameObject.SetActive(false);
    }
}