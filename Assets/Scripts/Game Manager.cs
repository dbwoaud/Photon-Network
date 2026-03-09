using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Unity.VisualScripting;
public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] double time;
    [SerializeField] double initializeTime;
    [SerializeField] int minute;
    [SerializeField] int second;
    [SerializeField] int millisecond;

    [SerializeField] Text timeText;

    void Awake()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("SetTime", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void SetTime()
    {
        initializeTime = PhotonNetwork.Time;
    }

    void Update()
    {
        time = (PhotonNetwork.Time - initializeTime);
        minute = (int)time / 60;
        second = (int)time % 60;
        millisecond = (int)(time * 100) % 100;
        timeText.text = string.Format("{0:00} : {1:00} : {2:00}", minute, second, millisecond);
    }
}
