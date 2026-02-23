using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class GeneratorPanel : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField inputField;
    [SerializeField] Toggle[] toggles;
    [SerializeField] int perssonal = 0;

    private void Awake()
    {
        toggles = GetComponentsInChildren<Toggle>();
    }
    void Start()
    {
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
        PhotonNetwork.CreateRoom(inputField.text, roomOptions);
        gameObject.SetActive(false);
    }
}
