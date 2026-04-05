using UnityEngine;
public class GrabHandler : MonoBehaviour
{
    public float grabRange = 2f;
    public KeyCode grabKey = KeyCode.G;
    public Transform holdPoint; 
    private GameObject grabbedObject;
    void Update()
    {
        if (Input.GetKeyDown(grabKey))
        {
            if (grabbedObject == null)
            {
                TryGrab();
            }
            else
            {
                Release();
            }
        }
    }
  void TryGrab()
{
    Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, grabRange);
    foreach (Collider col in nearbyColliders)
    {
        if (col.CompareTag("Grabbable") && col.gameObject != gameObject)
        {
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
    }
}
}