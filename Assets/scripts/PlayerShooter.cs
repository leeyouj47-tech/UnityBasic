using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public BulletMover prefab;
    public Transform camTransform;

    public void OnAttack()
    {
        if (camTransform == null) ;
        camTransform = transform.Find("Camera");
        Instantiate<BulletMover>(prefab, camTransform.position, camTransform.rotation);
    }

}
