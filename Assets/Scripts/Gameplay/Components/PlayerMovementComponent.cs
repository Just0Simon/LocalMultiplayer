using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Components
{
    public class PlayerMovementComponent : NetworkBehaviour
    {
        private const string MOVE_ACTION_NAME = "Move"; 
        
        [SerializeField] private float speed = 5f;

        [SerializeField]
        private PlayerInput _playerInput;
        [SerializeField]
        private InputActionAsset _inputActionMap;

        private InputAction _moveAction;

        private void Awake()
        {
            if (!IsLocalPlayer)
            {
                _playerInput.enabled = false;
                return;
            }
            
            _moveAction = _playerInput.actions.FindAction(MOVE_ACTION_NAME);
        }

        private void Update()
        {
            var rawMove = _moveAction.ReadValue<Vector2>();
            
            Vector3 move = new Vector3(rawMove.x, 0, rawMove.y).normalized * speed * Time.deltaTime;
            
            if (!IsOwner)
                return;

            transform.Translate(move);
            if (IsClient)
            {
                MoveServerRpc(move);   
            }
        }

        [ServerRpc(RequireOwnership = true)]
        private void MoveServerRpc(Vector3 translateVector)
        {
            transform.Translate(translateVector);
        }
    }
}