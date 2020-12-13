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
    private bool isGrounded = false;
    private float distanceToGround;
    private float rotationSensitivity = 1f;
    private bool isPoweradjusting = false;
    private Rigidbody rigidbody;
    private Vector3 lastStandstillPosition = Vector3.zero;

    private GameEndAndRestart gameEndAndRestart;
    private TrailRenderer trailRenderer;
    private AudioSource audioSource;

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

        gameEndAndRestart = FindObjectOfType<GameEndAndRestart>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(0, -0.1f, 0);
        distanceToGround = GetComponent<Collider>().bounds.extents.y;

        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.emitting = false;

        audioSource = GetComponent<AudioSource>();
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
            trailRenderer.emitting = true;
            standsStill = true;
            rigidbody.velocity = Vector3.zero;
            lastStandstillPosition = rigidbody.transform.position;
        }

        if (Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (standsStill && context.phase == InputActionPhase.Performed)
        {
            shotcounter.AddHit();
            isPoweradjusting = false;
            Vector3 shotDirection = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
            float power = Mathf.Max(10f, shotPower * powerbar.currentPowerNormalized);
            rigidbody.AddForce(shotDirection * power);
            audioSource.Play();
        }

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!standsStill && isGrounded)
        {
            rigidbody.AddForce(Vector3.up * 100f);
        }
    }

    public void Reset(InputAction.CallbackContext context)
    {
        ResetPosition();
    }

    public void AdjustPower(InputAction.CallbackContext context)
    {
        if (standsStill && context.phase == InputActionPhase.Started)
        {
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

    public void ResetPosition()
    {
        if (lastStandstillPosition == Vector3.zero)
        {
            gameEndAndRestart.FinishGame();
            gameEndAndRestart.NewGame();
        }

        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        Vector3 newPosition = lastStandstillPosition;
        newPosition.y = 2f;
        transform.position = newPosition;
    }
}
