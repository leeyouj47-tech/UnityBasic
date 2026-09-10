using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AGVController : MonoBehaviour
{
    public NavMeshAgent[] agents;
    public LayerMask mask;

    private bool isPressed = false;

    public void OnClick()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, mask))
        {
            Debug.Log($"{hit.transform.gameObject.name} => {hit.point}");

            foreach (NavMeshAgent agent in agents)
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
