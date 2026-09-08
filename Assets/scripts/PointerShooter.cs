using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    public BulletMover prefab;
    public Transform camTransform;

    public float shotDelay = 0.5f;

    private bool pullTrigger = false;
    private float fireTime;
    public void OnAttack(InputValue value)
    {
        pullTrigger = value.isPressed;

    }

    private void Update()
    {
        //Time.time 재생 누른 이후로 지나간 시간을 기록함.
        //공격 버튼을 누르고 있으면서, 발사시간이 현재시간보다 작아지면 발사.
        if (pullTrigger && fireTime < Time.time)
        {
            Instantiate<BulletMover>(prefab, camTransform.position, camTransform.rotation);
            fireTime = Time.time + shotDelay;
        }
    }

    private void Start()
    {
        if (camTransform == null)
            camTransform = transform.Find("Camera");
    }



}
