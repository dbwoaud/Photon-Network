using UnityEngine;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System.Collections;
public class PlayfabManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField addressInputField;
    [SerializeField] InputField passwordInputField;
    [SerializeField] string gameVersion;
    public void SuccessLogin(LoginResult loginResult)
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.GameVersion = gameVersion;
        StartCoroutine(ConnectRoutine());
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }
    private IEnumerator ConnectRoutine()
    {
        PhotonNetwork.ConnectUsingSettings();
        while(PhotonNetwork.IsConnectedAndReady == false)
        {
            yield return null;
        }

        PhotonNetwork.JoinLobby();

    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = addressInputField.text,
            Password = passwordInputField.text
        };

        PlayFabClientAPI.LoginWithEmailAddress
        (
            request,
            SuccessLogin,
            FailLogin
        );
    }

    public void CreateAccount()
    {
        PanelManager.Instance.Load(Panel.SUBSCRIBE);
    }
    
    public void FailLogin(PlayFabError playFabError)
    {
        PanelManager.Instance.Load(Panel.ERROR, playFabError.GenerateErrorReport());
    }
}
