using UnityEngine;
using UnityEngine.InputSystem;

public class Ch17Controller : MonoBehaviour
{
    public Transform camTransform;
    public Animator anim;
    public Vector2 direction;
    public bool isRunning;
    public float rotateSpeed = 360f;
    public TargetFollower follower;
    public Vector2 lookDelta;

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    public void OnSprint(InputValue value)
    {
        isRunning = value.isPressed;
    }
    public void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>();
    }
    private void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }
    private void LateUpdate()
    {
        if (follower != null)
            follower.Rotate(lookDelta);


        if (direction.magnitude > 0.1)
        {
            Vector3 forward = camTransform.forward;
            forward.y = 0f;
            forward = forward.normalized;
            Vector3 right = camTransform.right;
            right.y = 0f;
            right = right.normalized;

            Vector3 dir = forward * direction.y + right * direction.x;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.deltaTime);
        }

        anim.SetBool("IsRun", isRunning);
        anim.SetFloat("Speed", direction.magnitude);
    }
    /*private void LateUpdate()
    {
        if(direction.magnitude > 0.1)
        {
            if (follower != null)
                follower.Rotate(lookDelta);
            //y가 0인 이유: 캐릭터 시선에 따라 위아래로 카메라가 움직이는 것을 방지하기 위하여
            Vector3 forward = camTransForm.forward;
            forward.y = 0f;
            forward.Normalize();
            Vector3 right = camTransForm.right;
            right.y = 0f;
            right.Normalize();
            //대각선
            Vector3 dir = forward * direction.y + right * direction.x;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.deltaTime);
        }
        anim.SetBool("IsRun", isRunning);
        anim.SetFloat("Speed", direction.magnitude);
    }*/
}
