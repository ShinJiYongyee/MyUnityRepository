using Unity.Netcode.Components;
using UnityEngine;

public class ClientNetworkTransform : NetworkTransform
{
    // give client authority to renew transform 
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
