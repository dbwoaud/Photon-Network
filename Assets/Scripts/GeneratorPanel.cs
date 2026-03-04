using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class GeneratorPanel : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField inputField;
    [SerializeField] Button creatRoomButton;
    [SerializeField] Toggle[] toggles;
    [SerializeField] int perssonal = 0;

    private void Awake()
    {
        toggles = GetComponentsInChildren<Toggle>();
    }

    private void Start()
    {
        OnRoomNameChanged();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        inputField.text = "";
        for (int i = 0; i < toggles.Length; i++)
        {
            if (i == 0)
                toggles[i].isOn = true;
            else
                toggles[i].isOn = false;
        }
        Select(true);
    }
    public void Select(bool state)
    {
        if(!state)
            return;
        
        for(int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                perssonal = i + 2;
                break;
            }          
        }
    }
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = perssonal;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        if (inputField.text != "")
            PhotonNetwork.CreateRoom(inputField.text, roomOptions);
        else
            PhotonNetwork.CreateRoom("Basic", roomOptions);
        gameObject.SetActive(false);
    }

    public void OnRoomNameChanged()
    {
        creatRoomButton.interactable = !string.IsNullOrWhiteSpace(inputField.text);
    }
}
