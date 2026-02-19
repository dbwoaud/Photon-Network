using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
public class RoomData : MonoBehaviourPunCallbacks
{
    [SerializeField] Button button;
    [SerializeField] Text roomText;
    [SerializeField] string titleText;
    public void Start()
    {
        button.onClick.AddListener(() => PhotonNetwork.JoinRoom(titleText));
    }

    public void UpdateRoomInfo(RoomInfo roomInfo)
    {
        titleText = roomInfo.Name;
        roomText.text = roomInfo.Name + " ( " + roomInfo.PlayerCount + " / " + roomInfo.MaxPlayers + " )"; 
    }

    public void UpdateRoomStatus()
    {

    }
}
