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

    private void Start()
    {
        //커서 잠금과 보이지 않게 해주는 코드
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 

        if (camTransform == null )
        {
            camTransform = transform.Find("Camera");
        }
        if (camTransform != null)
        {
            camAngle = camTransform.eulerAngles.x;
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
        Vector3 dir;
        dir.x = direction.x;
        dir.y = 0f;
        dir.z = direction.y;
        transform.Translate(dir * moveSpeed * Time.deltaTime);
        //Translate 특정 위치에서 얼마만큼 이동하는지(local기준)
        /*transform.Translate(Vector3.forward * direction.y * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * direction.x * rotateSpeed * Time.deltaTime);*/
    }

    public void OnLook(InputValue value)
    {
        pointerDelta = value.Get<Vector2>();
    }

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    public void OnSpace()
    {
        Debug.Log("너 스페이스 눌렀지?");
    }
}