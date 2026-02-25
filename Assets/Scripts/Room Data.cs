using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using System;
public class RoomData : MonoBehaviourPunCallbacks
{
    [SerializeField] Button button;
    [SerializeField] Text roomText;
    [SerializeField] string titleText;
    [SerializeField] RoomInfo roomInfo;
    [SerializeField] event Action OnEntered;


    public override void OnEnable()
    {
        base.OnEnable();
        OnEntered += UpdateRoomStatus;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        OnEntered -= UpdateRoomStatus;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        PanelManager.Instance.Load(Panel.ERROR, message);
    }
    public void Start()
    {
        button.onClick.AddListener(() => PhotonNetwork.JoinRoom(titleText));
    }

    public void UpdateRoomInfo(RoomInfo roomInfo)
    {
        this.roomInfo = roomInfo;
        titleText = roomInfo.Name;
        roomText.text = roomInfo.Name + " ( " + roomInfo.PlayerCount + " / " + roomInfo.MaxPlayers + " )";
        OnEntered?.Invoke();
    }

    public void UpdateRoomStatus()
    {
        if (roomInfo.IsOpen)
            button.interactable = true;
        else
            button.interactable = false;

    }

}
