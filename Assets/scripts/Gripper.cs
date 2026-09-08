using UnityEngine;

public class Gripper : MonoBehaviour
{
    public Rigidbody grabbable;
    private Vector3 prevPosition;
    public Vector3 currentVelocity;

    private void OnTriggerEnter(Collider other)
    {
        grabbable = other.attachedRigidbody;
    }
    private void OnTriggerExit(Collider other)
    {
        if (grabbable.isKinematic == false && grabbable == other.attachedRigidbody)
        {
            grabbable = null;
        }
    }

    public void Grab()
    {
        if(grabbable == null)
            return;
            grabbable.transform.SetParent(transform);
            grabbable.isKinematic = true;
    }
    public void Release()
    {
        if (grabbable == null)
            return;
        grabbable.transform.SetParent(null);
        grabbable.isKinematic = false;
        grabbable.linearVelocity = currentVelocity;
        grabbable = null;
    }
    private void Start()
    {
        prevPosition = transform.position;
    }
    private void FixedUpdate()
    {
        currentVelocity = (transform.position - prevPosition) / Time.fixedDeltaTime;
        prevPosition = transform.position;
    }
}
