using UnityEngine;
using Photon.Pun;
public class Head : MonoBehaviourPunCallbacks
{
    [SerializeField] Rotation rotation;
    [SerializeField] float minAngle = -55f;
    [SerializeField] float maxAngle = 55f;

    public void Awake()
    {
        rotation = GetComponent<Rotation>();
    }

    private void Update()
    {
        if(photonView.IsMine)
        {
            rotation.RotateX(minAngle, maxAngle);
        }
    }
}
