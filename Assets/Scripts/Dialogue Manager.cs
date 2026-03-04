using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Chat;
public class DialogueManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField inputField;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] Transform parentTransform;

    [PunRPC]
    public void Send(string message)
    {
        GameObject chat = Instantiate(Resources.Load<GameObject>("Talk"), parentTransform);
        chat.GetComponent<Text>().text = message;
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0.0f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            inputField.ActivateInputField();
            if (inputField.text.Length <= 0)
            {
                return;
            }
            string talk = PhotonNetwork.LocalPlayer.NickName + ": " + inputField.text;
            photonView.RPC("Send", RpcTarget.All, talk);
            inputField.text = "";
            inputField.ActivateInputField();
        }
    }
}
