using UnityEngine;

public class FirstPersonAnimator : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        if (animator == null)
            Debug.LogError("FirstPersonAnimator: Animator not found.");
    }

    void Update()
    {
        if (animator == null) return;

        UpdateMovementAnimations();
        UpdateJumpAnimation();
    }

    private void UpdateMovementAnimations()
    {
        bool forward = Input.GetKey(KeyCode.W);
        bool sprint = Input.GetKey(KeyCode.LeftShift);
        bool crouch = Input.GetKey(KeyCode.LeftControl);

        animator.SetBool("isWalking",
            forward && !sprint && !crouch);

        animator.SetBool("isRunning",
            forward && sprint && !crouch);

        animator.SetBool("isCrouching",
            crouch);
    }

    private void UpdateJumpAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }
    }
}
