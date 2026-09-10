using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public GameObject effect;
    public Rigidbody rb;
    public float power = 100f;
    public float lifeTime = 5f;
    private float remainLife;

    public bool damagable = false;
    public LayerMask mask;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (damagable == false)
            return;

        Instantiate(effect, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
        Destroy(gameObject);
    }
}
