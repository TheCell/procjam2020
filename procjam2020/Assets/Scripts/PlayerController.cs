using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool IsPoweradjusting { get => isPoweradjusting; }

    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private float shotPower = 800f;
    [SerializeField]
    private Powerbar powerbar;
    [SerializeField]
    private ShotCounter shotcounter;

    private bool standsStill = false;
    private float rotationSensitivity = 1f;
    private bool isPoweradjusting = false;
    private Rigidbody rigidbody;

    public void Start()
    {
        if (cameraTransform == null)
        {
            Debug.LogError("no camera transform set");
        }
        if (powerbar == null)
        {
            Debug.LogError("missing powerbar");
        }

        rigidbody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if (standsStill || isPoweradjusting)
        {
            powerbar.setVisible(true);
        }
        else
        {
            powerbar.setVisible(false);
        }
    }

    public void FixedUpdate()
    {
        standsStill = false;

        if (rigidbody.velocity.magnitude < 0.05f)
        {
            standsStill = true;
            rigidbody.velocity = Vector3.zero;
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        // todo only when standstill
        if (context.phase == InputActionPhase.Performed)
        {
            //Debug.Log("fire");
            shotcounter.AddHit();
            isPoweradjusting = false;
            Vector3 shotDirection = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
            float power = Mathf.Max(10f, shotPower * powerbar.currentPowerNormalized);
            rigidbody.AddForce(shotDirection * power);
        }

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!standsStill)
        {
            // todo only when on ground
            rigidbody.AddForce(Vector3.up * 100f);
        }
    }

    public void Reset(InputAction.CallbackContext context)
    {
        //Debug.Log("reset");
        rigidbody.velocity = Vector3.zero;
        Vector3 newPosition = transform.position;
        newPosition.y = 2f;
        transform.position = newPosition;
    }

    public void AdjustPower(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("poweradjust started");
            isPoweradjusting = true;
        }
    }

    public void AdjustPowerDirection(InputAction.CallbackContext context)
    {
        if (isPoweradjusting)
        {
            Vector2 lookAroundValue = context.ReadValue<Vector2>();
            float deltaValue = lookAroundValue.x * Time.deltaTime * rotationSensitivity;
            float currentPower = powerbar.currentPowerNormalized;
            float newPower = Mathf.Clamp(currentPower + deltaValue, 0f, 1f);
            powerbar.currentPowerNormalized = newPower;
        }
    }
}
