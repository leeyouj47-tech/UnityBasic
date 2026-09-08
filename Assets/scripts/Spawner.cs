using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform[] spawnPosition;
    
    public void SpawnForward()
    {
        Instantiate<GameObject>(prefab, spawnPosition[0].position, spawnPosition[0].rotation);
    }
    public void SpawnBackward()
    {
        Instantiate<GameObject>(prefab, spawnPosition[1].position, spawnPosition[1].rotation);
    }
}
