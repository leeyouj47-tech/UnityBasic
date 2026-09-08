using UnityEngine;
using UnityEngine.InputSystem;

public class WASDMover : MonoBehaviour
{
    public Rigidbody rb;
    public Vector2 direction;
    public float moveSpeed = 3f;
    void Start()
    {
        //실수로 변수에 드래그해 넣지 않았을 경우 스크립트가 자동으로 가져와서 넣어줌.
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }


    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }
    //게임오브젝트가 처음 활성화되어 있을 때, 딱 한번만 호출.

    //활성화 되어 있는 동안 매프레임마다 호출.
    //void Update()
    //{
    //    Vector3 dir;
    //    dir.x = direction.x;
    //    dir.y = 0f;
    //    dir.z = direction.y;
    //    transform.Translate(dir * moveSpeed * Time.deltaTime);
    //}


    //물리 업데이트
    private void FixedUpdate()
    {
        Vector3 dir;
        dir.x = direction.x;
        dir.y = 0f;
        dir.z = direction.y;
        rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
    }
}
