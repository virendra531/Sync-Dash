using UnityEngine;

public class GhostController : MonoBehaviour
{
    public PlayerController player;
    private Rigidbody rb;
    private Vector3 targetPosition;
    public float smoothSpeed = 0.125f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 newTargetPosition = player.GetDelayedPosition();

        targetPosition = Vector3.Lerp(targetPosition, newTargetPosition, smoothSpeed);

        rb.MovePosition(targetPosition);
    }
}