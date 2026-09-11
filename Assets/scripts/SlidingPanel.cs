using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UIElements.Experimental;

public class SlidingPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector2 enterPosition;
    public Vector2 exitPosition;
    public float duration = 1f;
    public Ease easingType = Ease.Linear;
    public RectTransform rt;

    public SlidingButton[] buttons;

    public Texture2D icon;
    public Vector2 hotspot;

    public void OnPointerExit(PointerEventData eventData)
    {
        rt.DOAnchorPos(exitPosition, duration).SetEase(easingType).OnComplete(ExitButtons);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        rt.DOAnchorPos(enterPosition, duration).SetEase(easingType).OnComplete(EnterButtons); 
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(icon, hotspot, CursorMode.Auto);
    }

    private void EnterButtons()
    {
        foreach (SlidingButton button in buttons)
        {
            button.Enter();
        }
    }
    private void ExitButtons()
    {
        foreach(SlidingButton button in buttons)
        {
            button.Exit();
        }
    }
}
