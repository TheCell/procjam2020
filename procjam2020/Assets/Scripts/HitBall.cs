using UnityEngine;
using UnityEngine.InputSystem;

public class HitBall : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    float shotPower = 10f;
    private bool standsStill = false;
    private Rigidbody rigidbody;

    public void Start()
    {
        if (cameraTransform == null)
        {
            Debug.LogError("no camera transform set");
        }
        rigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        standsStill = false;
        if (rigidbody.velocity.magnitude < 0.1f)
        {
            standsStill = true;
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        //if (standsStill)
        if (true)
        {
            Vector3 shotDirection = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
            rigidbody.AddForce(shotDirection * shotPower);
            standsStill = true;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!standsStill)
        {
            rigidbody.AddForce(Vector3.up * 100f);
        }
    }

    public void Reset(InputAction.CallbackContext context)
    {
        rigidbody.velocity = Vector3.zero;
        Vector3 newPosition = transform.position;
        newPosition.y = 2f;
        transform.position = newPosition;
    }
}
