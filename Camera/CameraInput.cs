using UnityEngine;


public class CameraInput : LocalInputHandler
{
    [SerializeField] private CameraTarget _objectToFollow;

    [Header("Distance parameters")]
    [SerializeField] private float _maxDistance = 5.0f;
    [SerializeField] private float _minDistance = 1.5f;
    [SerializeField] private float _selectedDistance = 4.0f;

    [Header("Camera pitch")]
    [SerializeField] private float _pitchViewUp = 80.0f;
    [SerializeField] private float _pitchViewDown = -80.0f;

    [Header("Mouse move factor")]
    [SerializeField] private Vector2 _axiesFactor = new Vector2(2.35f, 1.15f);

    private float _currentDistance;
    private Vector3 _resultPosition;
    private Vector2 _angles;
    private Quaternion _additinalRotate = Quaternion.identity;
    private bool _isMoveFrozen = false;

    public override int ID => 1;
    public Vector2 Angles => _angles;
    public Quaternion AdditinalRotate => _additinalRotate;
    public bool IsMoveFrozen => _isMoveFrozen;

    private void Awake()
    {
        //attach distance input
        Cursor.visible = false;

        //Input.GetKey(KeyCode.mou);

    }

    private void Update()
    {
        const float AdditinalDistance = 0.40f;
        const float SpeedDistanceMove = 35.0f;

        Vector3 startCirclePosition = _objectToFollow.transform.position;
        Vector3 normalToActor;
        Vector3 followPos = _objectToFollow.transform.position;

        HandleInput();

        if (Physics.Linecast(followPos, followPos + followPos.GetNormalTo(transform.position) * _selectedDistance, out RaycastHit rayCastHit))
            _currentDistance = Mathf.Lerp(_currentDistance, rayCastHit.distance - AdditinalDistance, Time.unscaledDeltaTime * SpeedDistanceMove);
        else
            _currentDistance = Mathf.Lerp(_currentDistance, _selectedDistance, Time.unscaledDeltaTime * SpeedDistanceMove);

        _angles.x -= Input.GetAxis("Mouse X") * _axiesFactor.x;
        _angles.y -= Input.GetAxis("Mouse Y") * _axiesFactor.y;
        _angles.y = Mathf.Clamp(_angles.y, _pitchViewDown, _pitchViewUp);
        
        startCirclePosition.x -= _currentDistance * Mathf.Cos(_angles.x * Mathf.Deg2Rad);
        startCirclePosition.z -= _currentDistance * Mathf.Sin(_angles.x * Mathf.Deg2Rad);

        normalToActor = startCirclePosition.GetNormalTo(followPos);

        startCirclePosition += normalToActor * Mathf.Cos(_angles.y * Mathf.Deg2Rad) * (_currentDistance) + normalToActor * _currentDistance;
        startCirclePosition.y += Mathf.Sin(_angles.y * Mathf.Deg2Rad) * _currentDistance;
       
        _resultPosition = startCirclePosition;
    }

    private void FixedUpdate()
    {
        const float MoveSpeed = 100.0f;

        if (_isMoveFrozen == false)
            transform.position = Vector3.Slerp(transform.position, _resultPosition, Time.fixedUnscaledDeltaTime * MoveSpeed);

        transform.LookAt(_objectToFollow.transform);
        transform.rotation *= _additinalRotate;
    }

    public void SetDistance(float distance) => _selectedDistance = Mathf.Clamp(distance, _minDistance, _maxDistance);

    public void AddDistance(float distance) => _selectedDistance = Mathf.Clamp(_selectedDistance + distance, _minDistance, _maxDistance);

    public void SetRotate(Quaternion q) => _additinalRotate = q;

    public void SetMoveFrozen(bool isFrozen) => _isMoveFrozen = isFrozen;
}