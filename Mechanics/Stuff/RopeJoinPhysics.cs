using UnityEngine;

[RequireComponent(typeof(ActorPhysics))]
public class RopeJoinPhysics : MonoBehaviour
{
    [SerializeField] private Transform _hookTransform;
    private ActorPhysics _actorPhysics;
    private float _ropeLength;
    private bool _isPhysicsUsing = true;

    public bool IsPhysicsUsing => _isPhysicsUsing;
    public Transform HookTransform => _hookTransform;

    private void Awake()
    {
        _actorPhysics = GetComponent<ActorPhysics>();
    }

    private void FixedUpdate()
    {
        if (_isPhysicsUsing == false)
            return;

        float currentDistance = Vector3.Distance(_hookTransform.position, transform.position);
        Vector3 arcDirection = CalculateArcVelocity();    

        if (currentDistance > _ropeLength)
            _actorPhysics.SetVelocity(arcDirection);
    }

    private Vector3 CalculateArcVelocity()
    {
        const float ValidAngle = 90.0f;

        Vector3 normalToHook = transform.position.GetNormalTo(_hookTransform.position);
        Vector3 arcDirection = Vector3.Cross(_actorPhysics.Velocity, normalToHook).normalized * -1;

        return Quaternion.AngleAxis(ValidAngle, arcDirection) * normalToHook * _actorPhysics.VelocityValue;
    }

    public void SetLength(float length) => _ropeLength = length;

    public void SetHookTransform(Transform transform)
    {
        _hookTransform = transform;
        SetLength(Vector3.Distance(_hookTransform.position,transform.position));
    }

    public void UsePhysics(bool isEnabled) => _isPhysicsUsing = isEnabled;

}
