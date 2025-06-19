using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerDetector : NetworkBehaviour
    {
        public event Action<Player> ClosestPlayerChanged;
        
        [SerializeField]
        private float _radius = 5f;
        
        [SerializeField]
        private SphereCollider _sphereCollider;

        private Player _closestPlayer;
        private List<Player> _detectedPlayers = new List<Player>();
        
        private void OnTriggerEnter(Collider other)
        {
            if(!IsHost)
                return;
            
            var player = other.GetComponent<Player>();
            if(player == null)
                return;
            
            _detectedPlayers.Add(player);

            if (_closestPlayer == null)
            {
                _closestPlayer = player;
                ClosestPlayerChanged?.Invoke(player);
            }
        }

        private void Update()
        {
            if(_detectedPlayers.Count == 0)
                return;

            FindClosestPlayer();
        }

        private void FindClosestPlayer()
        {
            var closestPlayer = _detectedPlayers[0];
            var closestDistance = float.MaxValue;
            for (int i = _detectedPlayers.Count - 1; i >= 0; i--)
            {
                var player = _detectedPlayers[i];
                var distance = Vector3.Distance(closestPlayer.transform.position, player.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPlayer = player;
                }
            }
            
            if(_closestPlayer == closestPlayer)
                return;
            
            _closestPlayer = closestPlayer;
            ClosestPlayerChanged?.Invoke(closestPlayer);
        }

        private void OnValidate()
        {
            _sphereCollider = GetComponent<SphereCollider>();
            _sphereCollider.radius = _radius;
        }
    }
}