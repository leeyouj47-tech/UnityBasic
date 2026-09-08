using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public Rigidbody rb;
    public float power = 100f;
    public float lifeTime = 5f;
    private float remainLife;

    public bool selfDestroy = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        remainLife = Time.time + lifeTime;

        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddRelativeForce(Vector3.forward * power);
        }
    }

    private void Update()
    {
        if (selfDestroy && remainLife < Time.time)
            Destroy(gameObject);
    }


}
