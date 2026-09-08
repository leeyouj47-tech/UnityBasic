using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Color enterColor;
    private Color originColor;

    private void Start()
    {
        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        if (meshRenderer != null)
        {
            originColor = meshRenderer.material.color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag != "Player")
            return;

        if (other.transform.root.gameObject.name != "Player")
            return;

        meshRenderer.material.color = enterColor;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.tag != "Player")
            return;

        if (other.transform.root.gameObject.name != "Player")
            return;

        meshRenderer.material.color = originColor;
    }
}
