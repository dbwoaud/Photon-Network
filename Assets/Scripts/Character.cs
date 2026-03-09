using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
public class Character : MonoBehaviourPun
{
    [SerializeField] float speed;
    [SerializeField] Vector3 direction;
    [SerializeField] Rotation rotation;
    [SerializeField] Rigidbody rigidbody;
    private void Awake()
    {
        rotation = GetComponent<Rotation>();
        rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        DisableCamera();
    }

    private void FixedUpdate()
    {
        if(photonView.IsMine)
            Move();
    }

    void Update()
    {
        if(photonView.IsMine)
        {
            Pause();
            Control();;
            rotation.RotateY(rigidbody);
        }
    }

    private void Pause()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PanelManager.Instance.Load(Panel.PAUSE);
            MouseManager.Instance.SetMouse(true);
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
        rigidbody.MovePosition(rigidbody.position + rigidbody.transform.TransformDirection(direction) * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Item"))
            return;

        PhotonView view = other.GetComponent<PhotonView>();

        if(view.IsMine || PhotonNetwork.IsMasterClient)
            PhotonNetwork.Destroy(view.gameObject);
    }
}
