using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MeshRenderer meshRenderer;
    public Color enterColor = Color.red;
    private Color originColor;

    private void Start()
    {
        if (meshRenderer == null)
            meshRenderer = GetComponent<MeshRenderer>();

        if(meshRenderer != null)
            originColor = meshRenderer.material.color;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        meshRenderer.material.color = enterColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        meshRenderer.material.color = originColor;
    }
}
