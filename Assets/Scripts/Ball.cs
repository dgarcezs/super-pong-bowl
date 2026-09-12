using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BallController))]
public class Ball : MonoBehaviour
{
    private static readonly int IsMovingHash = Animator.StringToHash("is_moving");
    private Animator animator;
    private BallController ballController;


    private void Start()
    {
        animator = GetComponent<Animator>();
        ballController = GetComponent<BallController>();
    }

    private void Update()
    {
        animator.SetBool(IsMovingHash, ballController.IsMoving);
    }
}
