using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Planet : MonoBehaviour, IPointerClickHandler
{
    public Slider slider;
    public FollowPopup popup;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            //값에다가 0.1빼고 다시 value값에 넣는다는 뜻
            slider.value -= 0.1f;
            if(slider.value < 0.0001f)
            {
                Destroy(gameObject);
                Destroy(slider.gameObject);
            }
        }
        else if(eventData.button == PointerEventData.InputButton.Left)
        {
            popup.target = transform;
            popup.Open();
        }
    }
}
