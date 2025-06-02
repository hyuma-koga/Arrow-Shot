using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public Animator GetAnimator()
    {
        return animator;
    }

    public void PlayIdle()
    {
        animator.Play("Idle");
    }

    public void PlayShoot()
    {
        animator.SetTrigger("Trigger_Shoot");
    }

    public void PlayMove()
    {
        animator.SetTrigger("Trigger_Move");
    }
}
