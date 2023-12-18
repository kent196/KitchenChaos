using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    //Constant
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private Player player;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator != null)
        {
            animator.SetBool(IS_WALKING, player.IsWalking());
        }
    }
}
