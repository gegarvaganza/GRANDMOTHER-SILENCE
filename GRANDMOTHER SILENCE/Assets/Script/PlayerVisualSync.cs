using UnityEngine;

public class PlayerVisualSync : MonoBehaviour
{
    public Animator animator;
    public CharacterController controller; // HeroPlayer's CharacterController

    void Update()
    {
        float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;
        animator.SetFloat("Speed", speed);
    }
}
