using Photon.Pun;
using UnityEngine;
public class Character : MonoBehaviourPun
{
    [SerializeField] float speed;
    [SerializeField] Vector3 direction;

    void Update()
    {
        if(photonView.IsMine)
        {
            Control();
            Move();
        }   
    }

    public void Control()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");
        direction.Normalize();
    }

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
