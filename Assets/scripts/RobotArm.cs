using UnityEngine;

public class RobotArm : MonoBehaviour
{
    public Animator armAnim;
    public Gripper gripper;

    public void StartGrab()
    {
        armAnim.SetTrigger("Grab");
    }

    public void Grab()
    {
        gripper.Grab();
    }

    public void Release()
    {
        gripper.Release();
    }

    public void PowerOn()
    {
        armAnim.SetBool("IsOn", true);
    }

    public void PowerOff()
    {
        armAnim.SetBool("IsOn", false);
    }
}
