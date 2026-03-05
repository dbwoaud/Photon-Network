using UnityEngine;
using Photon.Pun;
using System.Collections;
using Photon.Realtime;
public class MasterManager : MonoBehaviourPunCallbacks
{
    private WaitForSeconds waitForSeconds = new WaitForSeconds(5.0f);
    private void Start()
    {
        StartCoroutine(SpawnPotionCoroutine());
    }

    IEnumerator SpawnPotionCoroutine()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            while(true)
            {
                if(PhotonNetwork.CurrentRoom != null)
                    PhotonNetwork.InstantiateRoomObject("Potion", Vector3.zero, Quaternion.identity);
                yield return waitForSeconds;
            }
        }
    }


    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);
        Debug.Log(newMasterClient.ToString());
    }
}
