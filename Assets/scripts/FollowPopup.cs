using DG.Tweening;
using UnityEngine;

public class FollowPopup : MonoBehaviour
{
    public Transform target;
    public RectTransform rt;
    public Vector3 offset;
    //{ get; set;} properties만드는 법
    public bool needScaleUp { get; set;}
    public float upSpeed = 3f;

    private void LateUpdate()
    {
        if (target != null)
            rt.position = Camera.main.WorldToScreenPoint(target.position) + offset;

        transform.localScale = Vector3.MoveTowards(transform.localScale, needScaleUp ? 
            Vector3.one:Vector3.zero, Time.deltaTime * upSpeed);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack);
    }
    public void Close()
    {
        transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.OutBack)
            .OnComplete(() => gameObject.SetActive(false));
    }
}
