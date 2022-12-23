using UnityEngine;

[RequireComponent(typeof(ActorPhysics))]
public class StateParkour : MonoBehaviour
{
    [SerializeField] private float _wallRunTimeMax = 1.5f;
    [SerializeField] private float _velocityToWall = 8.0f;
    [SerializeField] private float _verticalVelocityToWall = 5.0f;
    private ActorPhysics _physicsActor;
    private float _wallRunTime = 0.0f;

    public float WallRunTime => _wallRunTime;

    public bool IsRunOnWall => _wallRunTime > 0.0f;

    private void Awake()
    {
        _physicsActor = GetComponent<ActorPhysics>();  
    }

    private void Update()
    {
        WallRun();
    }

    public void WallRun()
    {
        bool isForwardHitted;
        bool isRightHitted;
        bool isLeftHitted;
        Vector2 xzMove = new Vector2(_physicsActor.Velocity.x, _physicsActor.Velocity.z);

        if(xzMove.magnitude > _velocityToWall && _physicsActor.Velocity.y > _verticalVelocityToWall)
        {
            isForwardHitted = Physics.Linecast(transform.position, transform.position + transform.forward);
            isRightHitted = Physics.Linecast(transform.position, transform.position + transform.right);
            isLeftHitted = Physics.Linecast(transform.position, transform.position - transform.right);

            if (isForwardHitted || isRightHitted || isLeftHitted)
            {
                _wallRunTime = _wallRunTimeMax;
                _physicsActor.SetGravity(ActorPhysics.DefaultGravity/5);
            }
        }

        if (_wallRunTime > 0.0f)
            _wallRunTime -= Time.deltaTime;

        if (_wallRunTime < 0)
        {
            _physicsActor.SetGravityByDefault();
        }
    }

}
