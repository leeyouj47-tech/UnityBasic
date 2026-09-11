using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShooter : MonoBehaviour
{
    public Toggle rapidToggle;
    public BulletMover prefab;
    public Transform camTransform;

    public float shotDelay = 0.5f;

    private bool pullTrigger = false;
    private float fireTime;

    private bool isRapidFire = false;
    public bool RapidFire
    {
        get => isRapidFire;
        //아래와 같은뜻
        //set => isRapidFire = value;
        set
        {
            isRapidFire = value;
            if(rapidToggle != null)
            {
                rapidToggle.isOn = value;
            }
        }
    }
    public void OnAttack(InputValue value)
    {
        if(!isRapidFire && value.isPressed)
        {
            Instantiate<BulletMover>(prefab, camTransform.position, camTransform.rotation);
        }
        pullTrigger = value.isPressed;
    }

    public void OnRapidFire()
    {
        Debug.Log("스페이스 눌렸니?");
        RapidFire = !RapidFire;
    }

    private void Update()
    {
        //Time.time 재생 누른 이후로 지나간 시간을 기록함.
        //공격 버튼을 누르고 있으면서, 발사시간이 현재시간보다 작아지면 발사.
        if (isRapidFire && pullTrigger && fireTime < Time.time)
        {
            Instantiate<BulletMover>(prefab, camTransform.position, camTransform.rotation);
            fireTime = Time.time + shotDelay;
        }
    }

    private void Start()
    {
        if (camTransform == null)
            camTransform = transform.Find("Camera");

        if(rapidToggle != null)
        {
            rapidToggle.isOn = isRapidFire;
        }
    }
}
