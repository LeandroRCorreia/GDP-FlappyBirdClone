using UnityEngine;

public class Pipe : MonoBehaviour
{

    [SerializeField] private Transform headTransform;

    public Vector3 Head => headTransform.position;
    
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer Sprite => spriteRenderer == null ?
    spriteRenderer = GetComponentInChildren<SpriteRenderer>()
    : spriteRenderer;



    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(Head, Vector3.one * 0.25f);
    }


}
