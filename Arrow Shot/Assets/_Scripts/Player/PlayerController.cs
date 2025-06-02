using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement movement;
    private BowController bow;
    private PlayerAnimationController animatorControl;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        bow = GetComponent<BowController>();
        animatorControl = GetComponent<PlayerAnimationController>();

        movement.SetAnimationController(animatorControl);
        bow.SetAnimationController(animatorControl);

        movement.SetBowController(bow);
    }

    private void Update()
    {
        movement.HandleMovement();
        bow.HandleShooting();
    }
}
