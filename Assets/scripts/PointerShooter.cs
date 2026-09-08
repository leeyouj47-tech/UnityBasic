using UnityEngine;
using UnityEngine.InputSystem;

public class PointerShooter : MonoBehaviour
{
    public GameObject prefab;
    public bool isPressed;
    public float rapidFireRate = 0.5f;
    private float fireTime;

    public void OnAttack(InputValue value)
    {
        isPressed = value.isPressed;
    }

    void Update()
    {
        //마우스가 눌러져 있지 않으면 리턴.
        if (isPressed == false)
            return;

        //발사가능 시간이 아니면 리턴
        if (fireTime > Time.time)
            return;

        //마우스 포인터의 위치값 읽어오기.
        //Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Instantiate<GameObject>(prefab, ray.origin, Quaternion.LookRotation(ray.direction));

        //다음 발사 시간 갱신
        fireTime = Time.time + rapidFireRate;
    }
}
