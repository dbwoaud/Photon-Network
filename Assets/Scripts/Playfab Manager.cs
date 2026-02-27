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
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), SuccessLogin, FailLogin);
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.GameVersion = gameVersion;
        StartCoroutine(ConnectRoutine());
    }

    public void SuccessLogin(GetAccountInfoResult getAccountInfoResult)
    {
        PhotonNetwork.LocalPlayer.NickName = getAccountInfoResult.AccountInfo?.Username;
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
        string errorMessage = null;
        var lines = playFabError.GenerateErrorReport().Split('\n');

        switch (lines.Length)
        {
            case 1:
                errorMessage = lines[0];
                break;
            case 2:
                errorMessage = lines[1];
                break;
            case >= 3:
                for (int i = 2; i < lines.Length; i++)
                    errorMessage += lines[i] + '\n';
                break;
            default:
                break;
        }
        PanelManager.Instance.Load(Panel.ERROR, errorMessage);
    }
}
