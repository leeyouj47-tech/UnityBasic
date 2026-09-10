using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Animator gearAnim;
    public Animator basicAnim;
    private float gearSpeed = 0f;
    private float gearDirection = 1f;
    public void OnGearUp()
    {
        gearSpeed = Mathf.Clamp(gearSpeed += 0.1f, 0f, 5f);
        gearAnim.SetFloat("Speed", gearSpeed * gearDirection);
    }

    public void OnGearDown()
    {
        gearSpeed = Mathf.Clamp(gearSpeed -= 0.1f, 0f, 5f);
        gearAnim.SetFloat("Speed", gearSpeed * gearDirection);
    }
    public void OnGearInvert()
    {
        gearDirection *= -1f;
        gearAnim.SetFloat("Speed", gearSpeed * gearDirection);
    }

    public void OnNext()
    {
        basicAnim.SetTrigger("Next");
    }
}
