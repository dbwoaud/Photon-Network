using UnityEngine;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class SubscribePanel : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField[] inputFields = new InputField[3];
    private void Awake()
    {
        inputFields = GetComponentsInChildren<InputField>();
    }

    public void Subscribe()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = inputFields[0].text,
            Email = inputFields[1].text,
            Password = inputFields[2].text,
        };

        PlayFabClientAPI.RegisterPlayFabUser
        (
            request,
            Success,
            Fail
        );
    }

    public void Success(RegisterPlayFabUserResult registerPlayFabUserResult)
    {
        gameObject.SetActive(false);
    }

    public void Fail(PlayFabError playFabError)
    {
        PanelManager.Instance.Load(Panel.ERROR, playFabError.GenerateErrorReport());
    }
}
