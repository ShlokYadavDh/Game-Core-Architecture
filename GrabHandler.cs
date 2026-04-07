using UnityEngine;

public class GrabHandler : MonoBehaviour
{
    [Header("Grab Settings")]
    public float grabRange = 2f;
    public KeyCode grabKey = KeyCode.G;
    public Transform holdPoint; 
    private GameObject grabbedObject;

    [Header("Strike Settings")]
    public KeyCode strikeKey = KeyCode.F;
    public Transform strikeZone; // The empty object with the Box Collider
    public Vector3 strikeHalfExtents = new Vector3(1f, 1f, 1f); // Size of hit area
    public float impactForce = 700f;

    // Pre-defined Reference to our State Manager
    private EntityStateManager stateManager;

    void Start()
    {
        stateManager = GetComponent<EntityStateManager>();
    }

    void Update()
    {
        // 1. GRAB LOGIC (Only if not striking)
        if (Input.GetKeyDown(grabKey) && stateManager.currentState != EntityState.Striking)
        {
            if (grabbedObject == null) TryGrab();
            else Release();
        }

        // 2. STRIKE LOGIC (Only if not already grabbing or striking)
        if (Input.GetKeyDown(strikeKey) && grabbedObject == null && stateManager.currentState == EntityState.Idle)
        {
            PerformStrike();
        }
    }

    void TryGrab()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, grabRange);
        foreach (Collider col in nearbyColliders)
        {
            if (col.CompareTag("Grabbable") && col.gameObject != gameObject)
            {
                stateManager.SetState(EntityState.Grabbing); // SET STATE
                grabbedObject = col.gameObject;
                Rigidbody targetRb = grabbedObject.GetComponent<Rigidbody>();

                if (targetRb != null) 
                {
                    targetRb.linearVelocity = Vector3.zero;
                    targetRb.angularVelocity = Vector3.zero;
                    targetRb.isKinematic = true; 
                    targetRb.detectCollisions = false; 
                }

                grabbedObject.transform.SetParent(holdPoint);
                grabbedObject.transform.localPosition = Vector3.zero;
                grabbedObject.transform.localRotation = Quaternion.identity;
                break;
            }
        }
    }

    void Release()
    {
        if (grabbedObject != null)
        {
            Rigidbody targetRb = grabbedObject.GetComponent<Rigidbody>();
            if (targetRb != null) 
            {
                targetRb.isKinematic = false;
                targetRb.detectCollisions = true; 
            }

            grabbedObject.transform.SetParent(null);
            grabbedObject = null;
            stateManager.SetState(EntityState.Idle); // RESET STATE
        }
    }

    void PerformStrike()
    {
        stateManager.SetState(EntityState.Striking);

        // Physics check in the StrikeZone area
        Collider[] hitObjects = Physics.OverlapBox(strikeZone.position, strikeHalfExtents);

        foreach (Collider col in hitObjects)
        {
            if (col.gameObject == gameObject) continue;

            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 pushDir = (col.transform.position - transform.position).normalized;
                rb.AddForce(pushDir * impactForce + Vector3.up * 150f);
                
                // Link to Respect System
                if(GetComponent<EntityStatus>()) 
                    GetComponent<EntityStatus>().GainRespect(2);
            }
        }

        // Return to Idle after 0.4 seconds 
        Invoke("ResetState", 0.4f);
    }

    void ResetState() { stateManager.SetState(EntityState.Idle); }
}