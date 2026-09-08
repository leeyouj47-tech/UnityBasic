using System;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    public Transform openPosition;
    public Transform closePosition;
    public float speed = 3f;

    public bool isOpen;

    public void Open()
    {
        isOpen = true;
    }
    public void Close()
    {
        isOpen = false;
    }

    private void Update()
    {
        transform.position =
            Vector3.MoveTowards(transform.position,
            isOpen ? openPosition.position : closePosition.position, speed * Time.deltaTime);
    }
}
