using UnityEngine;

public class DoorSensor : MonoBehaviour
{
    public AutomaticDoor[] doors;

    void Start()
    {
        foreach (var door in doors)
        {
            door.Close();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag != "Player")
            return;

        foreach (var door in doors)
        {
            door.Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.tag != "Player")
            return;

        foreach (var door in doors)
        {
            door.Close();
        }
    }


}
