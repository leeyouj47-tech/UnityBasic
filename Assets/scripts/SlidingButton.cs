using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class SlidingButton : MonoBehaviour
{
    public Vector2 enterPosition;
    public Vector2 exitPosition;
    public float duration;
    public Ease easingType = Ease.Linear;
    public float delay;
    public RectTransform rt;

    public void Enter()
    {
        rt.DOAnchorPos(enterPosition, duration)
            .SetEase(easingType)
            .SetDelay(delay);
    }
    public void Exit()
    {
        rt.anchoredPosition = exitPosition;
    }
}
