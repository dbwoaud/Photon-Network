using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();

    void Start()
    {
        SetSpawnPoint();
        Create();
    }

    public void Create()
    {
        PhotonNetwork.Instantiate("Character", spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].position, 
            spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].rotation);
    }

    public void SetSpawnPoint()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.MaxPlayers; i++) 
        {
            Transform clone = Instantiate(Resources.Load<Transform>("Spawn Point " + i));
            spawnPoints.Add(clone);
        }
    }
}
