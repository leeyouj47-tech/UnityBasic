using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    public Vector2 direction;               //키보드 입력
    public Vector2 pointerDelta;            //마우스 입력
    public float mouseSensitivity = 0.1f;       //마우스 감도
    public float camAngle = 0f;

    public float moveSpeed = 1f;

    public float rotateSpeed = 90f;

    public Transform camTransform;

    public Rigidbody rb;

    public float jumpHeight = 1f;   //점프 최대높이
    public bool isGrounded = false;  //땅 위에 있는지 여부
    public int maxJumpCount = 1;    //최대 연속 점프 횟수
    private int remainJumpCnt;  //점프 카운트
    public float groundRadius = 0.3f;
    public float groundOffset = 0f;
    public LayerMask groundMask;

    public bool IsGround
    {
        get => isGrounded;
        set
        {
            if(isGrounded == value) 
                return;

            if(isGrounded = value)
            {
                remainJumpCnt = maxJumpCount;
            }
        }
    }

    private void Start()
    {
        //커서 잠금과 보이지 않게 해주는 코드
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        if (camTransform == null )
        {
            camTransform = transform.Find("Camera");
        }
        if (camTransform != null)
        {
            camAngle = camTransform.eulerAngles.x;
        }
        if(rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }
    void Update()
    {
        //마우스 입력으로 상하 카메라 회전
        camAngle -= pointerDelta.y * mouseSensitivity * Time.deltaTime;
        camAngle = Mathf.Clamp(camAngle, -90f, +90f);
        camTransform.localRotation = Quaternion.Euler(camAngle, 0f, 0f);
        
        //마우스 입력으로 좌우회전
        transform.Rotate(Vector3.up * pointerDelta.x * mouseSensitivity * Time.deltaTime);

        //키보드 입력 벡터로 전후좌우 이동
       /* Vector3 dir;
        dir.x = direction.x;
        dir.y = 0f;
        dir.z = direction.y;
        transform.Translate(dir * moveSpeed * Time.deltaTime);*/
        //Translate 특정 위치에서 얼마만큼 이동하는지(local기준)
        /*transform.Translate(Vector3.forward * direction.y * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * direction.x * rotateSpeed * Time.deltaTime);*/
    }

    private void FixedUpdate()
    {
        IsGround = GroundCheck();

        Vector3 forward = camTransform.forward;
        forward.y = 0f;
        forward = forward.normalized;
        Vector3 right = camTransform.right;
        right.y = 0f;
        right = right.normalized;

        Vector3 dir = forward * direction.y + right * direction.x;
        rb.MovePosition(dir * moveSpeed * Time.fixedDeltaTime + rb.position);
    }

    private bool GroundCheck()
    {
        Vector3 gPos = transform.position;
        gPos.y += groundOffset;

        return Physics.CheckSphere(gPos, groundRadius, groundMask);
    }

    public void OnLook(InputValue value)
    {
        pointerDelta = value.Get<Vector2>();
    }

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    public void OnJump()
    {
        if(remainJumpCnt == 0)
            return;
        remainJumpCnt--;
        //sqrt = 루트
        //원하는 높이까지의 위치에너지를 운동에너지로 변환하는 공식을 이용해 위로 튀어오르게 함.
        rb.AddForce(Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y) * Vector3.up, ForceMode.VelocityChange);
    }

    //게임 오브젝트가 활성화 할때마다 매번 호출
    /*    private void OnEnable()
        {

        }*/
    //게임오브젝트가 비활성화 할때마다 매번 호출
    /*private void OnDisable()
    {
        
    }*/

    //게임 오브젝트가 파괴될때 호출
    /* private void OnDestroy()
     {
         camTransform.SetParent(null);
     }*/
    //게임 오브젝트가 선택되어 있을때만 그려지는 기즈모
    private void OnDrawGizmosSelected()
    {
        Color green = new Color(0, 1, 0, 0.35f);
        Color red = new Color(1, 0, 0, 0.35f);
        if(isGrounded)
            Gizmos.color = green;
        else
            Gizmos.color = red;

        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + groundOffset, transform.position.z), groundRadius);
    }
}