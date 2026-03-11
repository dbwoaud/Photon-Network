using UnityEngine;
using Photon.Pun;
public class Boost : MonoBehaviourPunCallbacks
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Animator animator;
    [SerializeField] float distance = 0.2f;
    [SerializeField] float force;
    private bool state;

    bool IsGrounded()
    {
        return Physics.Raycast(rigidbody.position + Vector3.up * 0.1f, Vector3.down, distance);
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (IsGrounded() && !state)
            animator.SetBool("Jump", false);

        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                state = true;
                animator.SetBool("Jump", true);
            }
        }
    }
    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            if (state)
            {
                rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, 0, rigidbody.linearVelocity.z);
                rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
                state = false;
            }
        }
    }


}
