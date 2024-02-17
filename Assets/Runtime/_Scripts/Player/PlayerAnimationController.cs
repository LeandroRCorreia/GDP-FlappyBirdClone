using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField] private Animator animator;

    private void Start()
    {
        playerController.playerDeathEvent += OnPlayeDeathEvent;
    }

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

    }

    void OnDestroy()
    {
        playerController.playerDeathEvent -= OnPlayeDeathEvent;
    }
    
    private void OnPlayeDeathEvent()
    {
        Die();
    }

    private void Die()
    {
        animator.SetFloat(PlayerAnimationConstants.FlyMultiplier, 0);

    }

}
