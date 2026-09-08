using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public int loadSceneID = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag != "Player")
            return;

        UnityEngine.SceneManagement.SceneManager.LoadScene(loadSceneID);
    }
}
