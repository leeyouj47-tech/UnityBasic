using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float range = 1.5f;
    public int damage = 1;
    public LayerMask mask;
    public float lifetime = 1f;
    private float remainTime;
    void Start()
    {
        remainTime = Time.time + lifetime;
        Collider[] cols = Physics.OverlapSphere(transform.position, range, mask);
        foreach(Collider col in cols)
        {
            Debug.Log(col.transform.root.gameObject.name);
            Health hp = col.transform.root.GetComponent<Health>();
            if(hp != null)
                hp.TakeDamage(damage);
        }
    }
    void Update()
    {
        if (remainTime > Time.time)
            return;
        Destroy(gameObject);
    }
}
