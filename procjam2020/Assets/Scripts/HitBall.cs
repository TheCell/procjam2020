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

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        standsStill = false;
        //Debug.Log(rigidbody.velocity.magnitude);
        if (rigidbody.velocity.magnitude < 0.1f)
        {
            standsStill = true;
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (standsStill)
        {
            Debug.Log("Fire");
            Vector3 shotDirection = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
            rigidbody.AddForce(shotDirection * shotPower);
            standsStill = true;
        }
    }
}
