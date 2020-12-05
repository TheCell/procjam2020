using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float smoothTime = 0.3f;
    public Vector3 smoothVelocity;

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 cameraOffset = Vector3.zero;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("missing target references");
        }
    }

    private void Update()
    {
        Transform tempTransform = target;
        tempTransform.rotation = Quaternion.identity;
        Vector3 targetPosition = tempTransform.TransformPoint(cameraOffset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothVelocity, smoothTime);
    }
}
