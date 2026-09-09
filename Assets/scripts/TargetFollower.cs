using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    public Transform target;
    public float mouseSensitivity = 1f;
    public float minAngle = -70f;
    public float maxAngle = 70f;
    public float distance = 3f;
    public Vector2 angles;

    public void Rotate(Vector2 delta)
    {
        angles.x = Mathf.Clamp(angles.x - delta.y * mouseSensitivity * Time.deltaTime, minAngle, maxAngle);
        angles.y += delta.x * mouseSensitivity * Time.deltaTime;
    }


    void Start()
    {
        angles = transform.eulerAngles;
        transform.position = -transform.forward * distance + target.position;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(angles.x, angles.y, 0f);
        transform.position = -transform.forward * distance + target.position;
    }

}
