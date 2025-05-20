using Unity.Netcode.Components;
using UnityEngine;

public class ClientNetworkAnimator : NetworkAnimator
{
    // give client authority to renew animation 
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
