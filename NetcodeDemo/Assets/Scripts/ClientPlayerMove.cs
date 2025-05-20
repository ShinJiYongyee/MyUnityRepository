using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using System;

public class ClientPlayerMove : NetworkBehaviour
{
    [SerializeField] private PlayerInput m_PlayerInput;
    [SerializeField] private StarterAssetsInputs m_StarterAssetsInputs;
    [SerializeField] private ThirdPersonController m_ThirdPersonController;

    private void Awake()
    {
        // disable update loop for components
        m_PlayerInput.enabled = false;
        m_StarterAssetsInputs.enabled = false;
        m_ThirdPersonController.enabled = false;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        // Indicates whether the local player is the owner of this networkbehaviour instance
        if (IsOwner)
        {
            // re-enable update loop if client owns player instance
            m_StarterAssetsInputs.enabled = true;
            m_PlayerInput.enabled = true;
        }
        if(IsServer)
        {
            m_ThirdPersonController.enabled = true;
        }
    }

    [Rpc(SendTo.Server)]
    private void UpdateInputServerRPC(Vector2 move, Vector2 look, bool jump, bool sprint)
    {
        // only sets values on server
        m_StarterAssetsInputs.MoveInput(move);
        m_StarterAssetsInputs.LookInput(look);
        m_StarterAssetsInputs.JumpInput(jump);
        m_StarterAssetsInputs.SprintInput(sprint);
    }

    private void LateUpdate()
    {
        if(!IsOwner)
        {
            return;
        }
        UpdateInputServerRPC(m_StarterAssetsInputs.move, m_StarterAssetsInputs.look, m_StarterAssetsInputs.jump, m_StarterAssetsInputs.sprint);
    }
}
