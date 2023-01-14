using UnityEngine;

[RequireComponent(typeof(RopeJoinPhysics))]
[RequireComponent(typeof(ActorSmoothLook))]
public class PlayerMove : ActorPhysics
{
    [SerializeField] private CameraInput _cameraInput;
    private ActorSmoothLook _smoothLook;
    private Vector3 _directionToMove = Vector3.zero;
    private MoveSet _walkMoveSet = new MoveSet(35, 3, 700, 1f, Vector3.down);
    private MoveSet _runMoveSet = new MoveSet(100, 6, 700, 1f, Vector3.down);
    private MoveSet _sprintMoveSet = new MoveSet(100, 20, 700, 1f, Vector3.down);
    private MoveSet _swingMoveSet = new MoveSet(155, 50, 700, 1f, Vector3.down);

    public MoveSet WalkMoveSet => _walkMoveSet;
    public MoveSet RunMoveSet => _runMoveSet;
    public MoveSet SprintMoveSet => _sprintMoveSet;

    protected override void OnAwake()
    {
        _smoothLook = GetComponent<ActorSmoothLook>();

        SetMoveSet(_runMoveSet);
    }

    protected override void OnUpdate()
    {
        HandleMove();
    }

    protected override void OnFixedUpdate()
    {
        
    }

    public void HandleMove()
    {
        const float LengthFactor = 1.50f;

        Vector3 cameraForwardVector;
        Vector3 moveDirection;

        if (_directionToMove.magnitude > 0.0f)
        {
            cameraForwardVector = _cameraInput.transform.position.Get2DNormalTo(transform.position);
            moveDirection = Quaternion.LookRotation(cameraForwardVector) * _directionToMove;

            _smoothLook.SetLookTarget(moveDirection);
            Move(GetDirectionalGroundSlope(moveDirection * LengthFactor));
            _directionToMove = Vector2.zero;
        }
    }

    public void MoveForward() => _directionToMove.z = 1;

    public void MoveBackward() => _directionToMove.z = -1;

    public void MoveLeft() => _directionToMove.x = -1;

    public void MoveRight() => _directionToMove.x = 1;

    public void GroundJump2()
    {
        if (IsOnGroundChached)
            Jump();
    }

    public void EnableSprintMode() => SetMoveSet(_sprintMoveSet);

    public void DisableSprintMode() => SetMoveSet(_runMoveSet);

    public void EnableSwingSprintMode() => SetMoveSet(_swingMoveSet);

    public void DisableSwingSprintMode() => SetMoveSet(_runMoveSet);

}
