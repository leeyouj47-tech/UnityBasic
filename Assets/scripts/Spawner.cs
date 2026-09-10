using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform[] spawnPositions;

    public void SpawnForward()
    {
        Instantiate<GameObject>(prefab, spawnPositions[0].position, spawnPositions[0].rotation);
    }
    public void SapwnBackward()
    {
        Instantiate<GameObject>(prefab, spawnPositions[1].position, spawnPositions[1].rotation);
    }

}
