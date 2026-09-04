using UnityEngine;

public class Mover : MonoBehaviour
{
    public Transform[] destination;
    public int index = 1;
    public float moveSpeed = 3f;

    void Start()
    {
        if(destination.Length > 0)
            transform.position = destination[0].position;
    }

    void Update()
    {
        //목적지로 이동
        //transform.position = destination;
        transform.position = Vector3.MoveTowards(transform.position, destination[index].position, moveSpeed * Time.deltaTime);
        if(transform.position == destination[index].position)
        {
            index++;
            if(index >= destination.Length)
            {
                index = 0;
            }
        }
        ;
    }
}
