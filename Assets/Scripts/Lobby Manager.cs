using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform parentTransform;
    [SerializeField] Dictionary<string,GameObject> roomDictionary = new Dictionary<string,GameObject>();

    public void GenerateRoom()
    {
        PanelManager.Instance.Load(Panel.GENERATOR);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
    public override void OnConnectedToMaster()
    {
        if(PhotonNetwork.InLobby == false)
            PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject roomObject = null;
        foreach (RoomInfo room in roomList)
        {
            if (room.RemovedFromList)
            {
                if (roomDictionary.TryGetValue(room.Name, out roomObject))
                {
                    roomDictionary.Remove(room.Name);
                    Destroy(roomObject);
                }             
            }
            else
            {
                if(!roomDictionary.TryGetValue(room.Name, out roomObject))
                {
                    roomObject = Instantiate(Resources.Load<GameObject>("Room"), parentTransform);
                    roomDictionary.Add(room.Name, roomObject);
                }
                roomObject.GetComponent<RoomData>().UpdateRoomInfo(room);
            }
        }
    }
}
