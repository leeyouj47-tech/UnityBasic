using UnityEngine;
using UnityEngine.EventSystems;

public class rotate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 rotateAxis = Vector3.up;
    public float rotateSpeed = 30f;
    public bool needRotate = false;


    // 처음 시작할 때 한번만 호출
    void Start()
    {
        Debug.Log($"{gameObject.name}이고, {transform.position}({transform.localPosition})");
    }

    // 매프레임마다 한번씩 호출
    void Update()
    {
        /*transform.position = Vector3.zero;
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;*/
        if (needRotate) {
            transform.Rotate(rotateAxis * rotateSpeed * Time.deltaTime);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        needRotate = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        needRotate = false;
    }
}