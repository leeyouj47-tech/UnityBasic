using UnityEngine;
using UnityEngine.AI;

public class Ch17AgentController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = agent.velocity;
        anim.SetFloat("Speed", velocity.magnitude);
    }
}
