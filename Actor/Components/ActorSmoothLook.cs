using UnityEngine;

[RequireComponent(typeof(ActorPhysics))]
public class ActorSmoothLook : MonoBehaviour
{
    [SerializeField] private float _angleSpeed = 1.0f;
    private ActorPhysics _physicsActor;
    private Quaternion _absoluteRotation = Quaternion.identity;  
    private Quaternion _targetRotation;
    private bool _isRotation = false;       
    private float _animationAngle = 0.0f;

    public Quaternion RotationTarget => _targetRotation;
    public Quaternion AbsoluteRotation => _absoluteRotation;

    private void Awake()
    {
        _physicsActor = GetComponent<ActorPhysics>();
    }

    private void FixedUpdate()
    {
        UpdateRotationByFixed();
    }
    //Curoutine? No
    private void UpdateRotationByFixed()
    {
        Quaternion resultRotation;

        if (_isRotation == true)
        {
            _animationAngle += Time.fixedDeltaTime * _angleSpeed;
            resultRotation = Quaternion.Slerp(_physicsActor.Rotation, _targetRotation, _animationAngle);
            _physicsActor.MoveRotation(resultRotation);

            if (_animationAngle > 1.0f)
            {
                _animationAngle = 0;
                _isRotation = false;
            }
        }
    }

    public void SetLookTarget(Vector3 direction)
    {
        _isRotation = true;
        _targetRotation = _absoluteRotation * Quaternion.LookRotation(direction);
        _animationAngle = 0.0f;
    }

    public void SetAbsoluteRotation(Quaternion rotate) => _absoluteRotation = rotate;
         
}
