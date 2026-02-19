using UnityEngine;
using Photon.Pun;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public override void OnConnectedToMaster()
    {
        if(PhotonNetwork.InLobby == false)
            PhotonNetwork.JoinLobby();
    }
    
    public override void OnRoomListUpdate
}
