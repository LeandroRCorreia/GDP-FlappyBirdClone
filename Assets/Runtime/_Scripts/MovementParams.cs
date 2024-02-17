using UnityEngine;

[CreateAssetMenu(fileName = "NewMovementParams", menuName = "Game/Movement")]
public class MovementParams : ScriptableObject
{
    [SerializeField] [Range(0, 15f)] private float upSpeed = 7f;
    [SerializeField] [Range(0f, 10f)] private float forwardSpeed = 3f;

    [SerializeField] [Range(0, 90f)] private float flapAngleDegress = 25;
    [SerializeField] private float rotateDownSpeed = 150f;
    [SerializeField] private float gravity = 9.8f;

    public float UpSpeed => upSpeed;
    public float ForwardSpeed => forwardSpeed;
    public float FlapAngleDegress => flapAngleDegress;
    public float RotateDownSpeed => rotateDownSpeed;
    public float Gravity => gravity;

}
