using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform follow;


    private void LateUpdate()
    {
        Vector3 position = new Vector3(follow.position.x, transform.position.y, transform.position.z);

        transform.position = position;


    }


}
