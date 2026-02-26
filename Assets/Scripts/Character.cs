using Photon.Pun;
using UnityEngine;
public class Character : MonoBehaviourPun
{
    [SerializeField] float speed;
    [SerializeField] Vector3 direction;
    [SerializeField] Rotation rotation;
    [SerializeField] Head head;
    private void Awake()
    {
        rotation = GetComponent<Rotation>();
        head = GetComponent<Head>();
    }
    private void Start()
    {
        DisableCamera();
    }
    void Update()
    {
        if(photonView.IsMine)
        {
            Control();
            Move();
            rotation.RotateY();
            head.RotateX();
        }
    }

    public void DisableCamera()
    {
        if(photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            Camera eye = transform.GetComponentInChildren<Camera>();
            eye.GetComponent<AudioListener>().gameObject.SetActive(false);
            eye.gameObject.SetActive(false);
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
