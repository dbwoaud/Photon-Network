using UnityEngine;

public class Boost : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] float force;
    private bool state;

    bool IsGrounded()
    {
        RaycastHit hitInfo;
        Vector3 startPos = rigidbody.position;
        Physics.Raycast(startPos, Vector3.down, out hitInfo);
        if (hitInfo.distance < 0.3f)
            return true;
        else
            return false;
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state = true;
        }
    }
    void FixedUpdate()
    {
        if (state && IsGrounded())
        {
            rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, 0, rigidbody.linearVelocity.z);
            rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
            state = false;
        }
    }


}
