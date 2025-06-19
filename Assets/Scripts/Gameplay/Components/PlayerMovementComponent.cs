using Unity.Netcode;
using UnityEngine;

namespace Gameplay.Components
{
    public class PlayerMovementComponent : NetworkBehaviour
    {
        [SerializeField] private float speed = 5f;

        private void Update()
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");
            Vector3 move = new Vector3(moveX, 0, moveZ).normalized * speed * Time.deltaTime;
            
            if (!IsOwner)
                return;

            transform.Translate(move);
            if (IsClient)
            {
                MoveServerRpc(move);   
            }
        }

        [ServerRpc(RequireOwnership = true)]
        public void MoveServerRpc(Vector3 translateVector)
        {
            transform.Translate(translateVector);
        }
    }
}