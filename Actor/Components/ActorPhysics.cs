using UnityEngine;

[System.Serializable]
public struct MoveSet
{
    [Tooltip("Acceleration to move actor using gravity")]
    [SerializeField] private float _accelerationSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _moveInAirFactor;
    [SerializeField] private Vector3 _gravityVector;

    public float AccelerationSpeed => _accelerationSpeed;
    public float MaxSpeed => _maxSpeed;
    public float JumpHeight => _jumpHeight;
    public float MoveInAirFactor => _moveInAirFactor;
    public Vector3 GravityVector => _gravityVector;

    public MoveSet(float accelerationSpeed, float maxSpeed, float jumpHeight, float moveInAirFactor, Vector3 gravityVector)
    {
        _accelerationSpeed = accelerationSpeed;
        _maxSpeed = maxSpeed;
        _jumpHeight = jumpHeight;
        _moveInAirFactor = moveInAirFactor;
        _gravityVector = gravityVector;
    }
}

[RequireComponent(typeof(Rigidbody))]
public abstract class ActorPhysics : MonoBehaviour
{
    public static readonly Vector3 DefaultGravity = Vector3.down * 1200;
    public const float NoVelocity = 0.01f;

    [Header("Moveable params")]
    [SerializeField] private MoveSet _moveSet = new MoveSet(1, 10, 1000, 0.4f, Vector3.down);
    [SerializeField] float _groundVectorFactor = 1.25f;
    private bool _canMove = false;  
    private bool _canJump = false;
    private Vector3 _gravityDirection = DefaultGravity;
    private Vector3 _prevVelocity;
    private Vector3 _velocityXZ;
    private Rigidbody _rigidbody;
    private bool _isOnGround;
    private float _savedDrag;

    public Transform Transform => transform;
    public MoveSet CurrentMoveSet => _moveSet;
    public Vector3 Velocity => _rigidbody.velocity;
    public Vector3 PrevVelocity => _prevVelocity;
    public Vector3 VelocityXZ => _velocityXZ;
    public float VelocityXZValue => _velocityXZ.magnitude;
    public float VelocityValue => Velocity.magnitude;
    public Quaternion Rotation => _rigidbody.rotation;
    public bool IsOnGroundChached => _isOnGround;
    public bool CanMove => _canMove;
    public bool CanJump => _canJump;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _savedDrag = _rigidbody.drag;
        OnAwake();
    }

    private void Update()
    {
        _velocityXZ = GetVelocityXZ();
        OnUpdate();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate();
        UseGravity();
        _isOnGround = IsOnGround();
        _canMove = true;
        _canJump = true;
        _prevVelocity = Velocity;

        if (_isOnGround == true)
            SetDrag(_savedDrag);
        else
            SetDrag(0.25f);
    }

    protected abstract void OnAwake();

    protected abstract void OnUpdate();

    protected abstract void OnFixedUpdate();

    public void AddForce(Vector3 forceVec, ForceMode mode) => _rigidbody.AddForce(forceVec, mode);

    public void SetVelocity(Vector3 velocity) => _rigidbody.velocity = velocity;

    public Vector3 GetVelocityXZ()
    {
        Vector3 velocity = Velocity;
        velocity.y = 0.0f;

        return velocity;
    }

    public void MoveRotation(Quaternion rotation) => _rigidbody.MoveRotation(rotation);

    public bool IsOnGround()
    {
        const float NoValidVelocity = 1.25f;

        return Mathf.Abs(Velocity.y - PrevVelocity.y) < NoValidVelocity &&  
            Physics.Linecast(transform.position, transform.position - transform.up * _groundVectorFactor);
    }

    public void SetGravityByDefault() => _gravityDirection = DefaultGravity;

    public void SetGravity(Vector3 vector) => _gravityDirection = vector;

    public void SetDrag(float dragValue) => _rigidbody.drag = dragValue;

    public void UseGravity()
    {
        _rigidbody.AddForce(_gravityDirection * Time.fixedDeltaTime);
    }

    public void Move(Vector3 direction)
    {
        float limitedSpeed;
        Vector3 resultForce;

        if (_canMove == true)
        {
            limitedSpeed = (_moveSet.MaxSpeed - VelocityXZValue) / _moveSet.MaxSpeed;
            limitedSpeed = limitedSpeed < 0.0f ? 0.0f : limitedSpeed;
            //limitedSpeed *= IsOnGroundChached == false ? _moveSet.MoveInAirFactor : 1.0f;
            resultForce = direction * _moveSet.AccelerationSpeed * limitedSpeed;

            _rigidbody.AddForce(resultForce, ForceMode.Acceleration);

            _canMove = false;
        }
    }

    public void Jump()
    {
        if (_canJump == true)
        {
            _rigidbody.AddForce(transform.up * _moveSet.JumpHeight);
            _canJump = false;
        }
    }

    public void SetMoveSet(MoveSet moveSet) => _moveSet = moveSet;

    public Vector3 GetDirectionalGroundSlope() => GetDirectionalGroundSlope(transform.forward);

    public Vector3 GetDirectionalGroundSlope(Vector3 direction)
    {
        const float ValidAngle = -89;

        Vector3 righthand = new Vector3(direction.z,direction.y,-direction.x);
        direction = Quaternion.AngleAxis(ValidAngle, righthand) * direction;

        return PhysicsExtended.SurfaceCastV2(transform.position, transform.position - direction, 45, 180);
    }
}
