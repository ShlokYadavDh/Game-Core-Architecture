using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f; // High speed to make sure we see it move
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Safety Check: If you forgot to add a Rigidbody, the code will add one for you!
        if (rb == null) {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Freeze rotation so the cube doesn't tip over
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        // Simple WASD / Arrow Key detection
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) moveZ = 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) moveZ = -1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveX = 1f;

        Vector3 direction = new Vector3(moveX, 0, moveZ).normalized;

        // Move the Rigidbody
        rb.linearVelocity = new Vector3(direction.x * moveSpeed, rb.linearVelocity.y, direction.z * moveSpeed);

        // Rotate the Cube to face the direction of movement
        if (direction != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * 10f);
        }
    }
}