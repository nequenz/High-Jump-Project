using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private Transform _targetToAttach;
    [SerializeField] private CameraInput _cameraInput;
    [SerializeField] private float _distance;
    [SerializeField] private float _moveSpeed = 50.0f;
    private float _currentDistance;
    private bool _isManualControl = false;
    private Vector3 _manualPosition;

    public Transform AttachedTarget => _targetToAttach;
    public bool IsManualControl => _isManualControl;
    public Vector3 ManualPosition => _manualPosition;
    public float MoveSpeed => _moveSpeed;

    private void FixedUpdate()
    {
        const float OffsetDistanceSpeed = 3.0f;
        const float ValidRotate = -90.0f;

        Vector3 newPosition;
        float angleX;

        if (_cameraInput is null)
            return;

        _currentDistance = Mathf.Lerp(_currentDistance, CalculateValidDistance(), Time.deltaTime * OffsetDistanceSpeed);

        angleX = _cameraInput.Angles.x + ValidRotate;

        newPosition = _isManualControl ? _manualPosition : _targetToAttach.position;
        newPosition.x -= _currentDistance * Mathf.Cos(angleX * Mathf.Deg2Rad);
        newPosition.z -= _currentDistance * Mathf.Sin(angleX * Mathf.Deg2Rad);

        transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.fixedDeltaTime * _moveSpeed);
    }

    private float CalculateValidDistance()
    {
        Vector3 rightHandVector = _targetToAttach.position.GetNormalTo(transform.position);

        return Physics.Linecast(_targetToAttach.position, _targetToAttach.position + rightHandVector, out RaycastHit hit) 
            ? hit.distance - 0.50f : _distance;
    }

    public void AttachObject(Transform objectToAttach) => _targetToAttach = objectToAttach;

    public void SetManualControl(bool isEnabled) => _isManualControl = isEnabled;

    public void SetManualPosition(Vector3 newPosition) => _manualPosition = newPosition;

    public void SetMoveSpeed(float moveSpeed) => _moveSpeed = moveSpeed;

}
