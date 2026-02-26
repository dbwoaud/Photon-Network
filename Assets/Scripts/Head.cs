using UnityEngine;
using Photon.Pun;
public class Head : MonoBehaviourPun
{
    [SerializeField] float axis;
    [SerializeField] float speed;

    public void RotateX()
    {
        axis -= Input.GetAxisRaw("Mouse Y") * speed * Time.deltaTime;
        axis = Mathf.Clamp(axis, -55f, 55f);
        transform.localEulerAngles = new Vector3(axis, 0, 0);
    }
}
