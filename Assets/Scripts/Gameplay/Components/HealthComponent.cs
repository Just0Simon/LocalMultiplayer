using System;
using Unity.Netcode;
using UnityEngine;

namespace Gameplay.Components
{
    public class HealthComponent : NetworkBehaviour
    {
        public event Action<int> HealthChanged; 
        
        [SerializeField]
        private int _baseHealth = 100;

        private int Health
        {
            get => _health.Value;
            set
            {
                _health.Value = value;
                HealthChanged?.Invoke(value);
            }
        }
        
        private NetworkVariable<int> _health;

        private void Awake()
        {
            _health = new NetworkVariable<int>(_baseHealth);
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                Debug.LogError("Damage cannot be negative");
                return;
            }
            
            Health -= damage;
        }

        public void RestoreHealth(int restoreAmount)
        {
            if (restoreAmount < 0)
            {
                Debug.LogError("Restore health amount cannot be negative");
                return;
            }
            
            Health += restoreAmount;
        }
    }
}