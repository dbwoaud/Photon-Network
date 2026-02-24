using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using TMPro.EditorUtilities;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();

    private void Awake()
    {
        spawnPoints.Clear();
        for(int i = 0; i < 4; i++)
            spawnPoints.Add(((GameObject)Resources.Load("Spawn Point" + i.ToString())).transform);
    }
    void Start()
    {
        Create();
    }

    public void Create()
    {
        PhotonNetwork.Instantiate("Character",Vector3.zero, Quaternion.identity);
        for(int i = 0; i < PhotonNetwork.CurrentRoom.MaxPlayers; i++)
            Instantiate(spawnPoints[i], spawnPoints[i].position, Quaternion.identity);
    }
}
