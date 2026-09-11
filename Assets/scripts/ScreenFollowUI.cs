using UnityEngine;

public class ScreenFollowUI : MonoBehaviour
{
    public RectTransform rt;
    public Transform target;
    public Vector3 offset;

    private void LateUpdate()
    {
        if(target != null)
        rt.position = Camera.main.WorldToScreenPoint(target.position) + offset;
    }
}
