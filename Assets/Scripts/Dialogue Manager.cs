using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField inputField;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] Transform parentTransform;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            inputField.ActivateInputField();
            if (inputField.text.Length <= 0)
                return;
        
            string contents = $"<color=black>{PhotonNetwork.LocalPlayer.NickName} : {inputField.text}</color>";
            photonView.RPC("Send", RpcTarget.All, contents);
            inputField.text = "";
            inputField.ActivateInputField();
        }
    }

    [PunRPC]
    public void Send(string message)
    {
        GameObject talk = Instantiate(Resources.Load<GameObject>("Talk"));
        talk.GetComponent<Text>().text = message;
        talk.transform.SetParent(parentTransform);
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0.0f;
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        string contents = $"<color=green>{newPlayer.NickName}¥‘¿Ã ¬¸∞°«ﬂΩ¿¥œ¥Ÿ.</color> ";
        photonView.RPC("Send", RpcTarget.All, contents);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        string contents = $"<color=red>{otherPlayer.NickName}¥‘¿Ã ∂∞≥µΩ¿¥œ¥Ÿ.</color> ";
        photonView.RPC("Send", RpcTarget.All, contents);
    }
}