using UnityEngine;
using Photon.Pun;

public class Rotation : MonoBehaviourPun
{
    [SerializeField] float axis;
    [SerializeField] float speed;


    public void RotateX(float minAngle, float maxAngle)
    {
        axis += Input.GetAxisRaw("Mouse Y") * speed * Time.deltaTime;
        axis = Mathf.Clamp(axis, minAngle, maxAngle);
        transform.localEulerAngles = new Vector3(-axis, 0, 0);
    }
    public void RotateY()
    {
        axis += Input.GetAxisRaw("Mouse X") * speed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, axis, 0);
    }
}
