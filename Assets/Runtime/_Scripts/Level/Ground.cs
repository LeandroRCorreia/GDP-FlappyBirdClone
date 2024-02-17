using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private MovementParams frozenMovement;
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    private SpriteRenderer spriteRenderer;

    public SpriteRenderer Sprite => spriteRenderer == null 
    ? spriteRenderer = GetComponentInChildren<SpriteRenderer>()
    : spriteRenderer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out PlayerController playerController))
        {
            OnHitGround(playerController);

        }
        
    }

    private void OnHitGround(PlayerController playerController)
    {
        playerController.CurrentMovementParams = frozenMovement;
        playerController.StopImediatelly();
        playerController.OnHitGround();
    }

}
