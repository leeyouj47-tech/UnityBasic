using UnityEngine;

public class FollowUI : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    void Start()
    {
        GameObject go = GameObject.Find("Canvas_World");
        if(go != null)
        {
            transform.SetParent(go.transform);
        }
    }
    //업데이트가 전부 호출된 이후 호출됨
    //애니메이션, UI, 카메라는 해당으로 호출
    private void LateUpdate()
    {
        if(target == null)
        {
            Destroy(gameObject);
        }
        transform.position = target.position + offset;
        //하늘을 쳐다보는 것에 대해서 자세하게 수정하는 방법이 Vector3.up 넣는 것.
        //transform.rotation = Quaternion.LookRotation((transform.position - Camera.main.transform.position).normalized, Vector3.up);
        transform.rotation = Camera.main.transform.rotation;
    }
    void Update()
    {
        
    }
}
