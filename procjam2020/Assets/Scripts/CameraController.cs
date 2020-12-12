using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float smoothTime = 0.3f;
    public Vector3 smoothVelocity;
    public float rotationSensitivity = 70f;

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 cameraOffset = Vector3.zero;
    [SerializeField]
    PlayerController playerController;

    private Quaternion originalRotation = Quaternion.identity;
    private float currentAngle = 0f;
    private float addDelta;

    public void Start()
    {
        if (target == null)
        {
            Debug.LogError("missing target references");
        }
        if (playerController == null)
        {
            Debug.LogError("playercontroller reference is missing, needed to disable lookaround");
        }

        originalRotation = transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {
        CalculateCurrentRotation();
        CameraFollow();
    }

    public void LookAround(InputAction.CallbackContext context)
    {
        Vector2 lookAroundValue = context.ReadValue<Vector2>();
        addDelta = lookAroundValue.x;
    }

    private void CalculateCurrentRotation()
    {
        if (!playerController.IsPoweradjusting)
        {
            currentAngle += addDelta * Time.deltaTime * rotationSensitivity;
        }
    }

    private void CameraFollow()
    {
        transform.position = target.position;
        Quaternion currentRotationQuaternion = Quaternion.Euler(0f, currentAngle, 0f);
        transform.rotation = currentRotationQuaternion * originalRotation;
        transform.position = transform.TransformPoint(cameraOffset);
    }

}
