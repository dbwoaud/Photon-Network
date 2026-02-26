using UnityEngine;
using Photon.Pun;

public class Rotation : MonoBehaviourPun
{
    [SerializeField] float axis;
    [SerializeField] float speed;

    public void RotateY()
    {
        axis += Input.GetAxisRaw("Mouse X") * speed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, axis, 0);
    }
}
