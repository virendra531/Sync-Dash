using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI speedText;
    public float baseSpeed = 5f;
    public float speedIncreaseRate = 0.1f;
    public float maxSpeed = 1500f;
    public float currentSpeed;
    public float jumpForce = 5f;
    public ObjectPool orbPool;
    public GameObject orbBurstPrefab;
    public float recordInterval = 0.05f; // Finer interval for smooth ghost movement
    public float delayValue = 1f; // Maximum delay for ghost
    private float ghostDelay; // Random delay set at start
    public System.Collections.Generic.List<PlayerState> stateHistory = new System.Collections.Generic.List<PlayerState>();
    private Rigidbody rb;
    private float timer = 0f;
    private bool isGrounded;
    private bool isJumping;

    [System.Serializable]
    public struct PlayerState
    {
        public float time;
        public Vector3 position;
        public bool jumped;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = baseSpeed;
        ghostDelay = Random.Range(0f, delayValue);
        isGrounded = true;
        isJumping = false;
    }

    void FixedUpdate()
    {
        float forwardSpeed = isJumping ? baseSpeed : currentSpeed;
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, forwardSpeed);
    }

    void Update()
    {
        // Increase speed over time
        currentSpeed = Mathf.Min(currentSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);
        // Update speed display
        speedText.text = "Speed: " + currentSpeed.ToString("F2");

        // Check if grounded using raycast
        bool wasGrounded = isGrounded;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.6f); // Adjust distance based on cube size

        // Detect landing to reset jump state
        if (!wasGrounded && isGrounded)
        {
            isJumping = false;
        }

        // Jump only if grounded and input received
        bool jumped = false;
        bool jumpKeyPressed = (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame) ||
                              (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame) ||
                              (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame);
        if (isGrounded && jumpKeyPressed)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
            jumped = true;
        }

        // Record state
        timer += Time.deltaTime;
        if (timer >= recordInterval)
        {
            timer = 0f;
            stateHistory.Add(new PlayerState { time = Time.time, position = transform.position, jumped = jumped });
            if (stateHistory.Count > 200) stateHistory.RemoveAt(0); // Increased cap for finer data
        }
    }

    public Vector3 GetDelayedPosition()
    {
        float targetTime = Time.time - ghostDelay;
        if (stateHistory.Count == 0) return transform.position; // Fallback to current position

        for (int i = 0; i < stateHistory.Count - 1; i++)
        {
            if (stateHistory[i].time <= targetTime && targetTime < stateHistory[i + 1].time)
            {
                float t = (targetTime - stateHistory[i].time) / (stateHistory[i + 1].time - stateHistory[i].time);
                return Vector3.Lerp(stateHistory[i].position, stateHistory[i + 1].position, t);
            }
        }
        return stateHistory[stateHistory.Count - 1].position; // Use latest if no match
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.instance.GameOver();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Orb"))
        {
            GameManager.instance.AddScore(1);
            orbPool.ReturnObject(other.gameObject);
            Instantiate(orbBurstPrefab, other.transform.position, Quaternion.identity);
        }
    }
}