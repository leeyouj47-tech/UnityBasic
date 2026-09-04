using UnityEngine;
using UnityEngine.UIElements;

public class BoxManager : MonoBehaviour
{
    public GameObject[] boxes;
    public rotate[] rotaters;
    public float delayTime = 1f;
    private float passedTime;
    private bool needActivate = true;
    
    void Update()
    {
        //passedTime = passedTime + Time.deltaTime;을 줄여서 쓴 부분이 아래
        passedTime += Time.deltaTime;
        if (needActivate && passedTime > delayTime)
        {
            foreach (GameObject go in boxes)
            {
                go.SetActive(true);
            }
            needActivate = false;
        }
    }
}