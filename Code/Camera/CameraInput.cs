using UnityEngine;


public class CameraInput : LocalInputHandler
{
    [SerializeField] private GameObject _objectToFollow;
    [SerializeField] private float _maxDistance = 5.0f;
    [SerializeField] private float _minDistance = 1.5f;
    [SerializeField] private float _selectedDistance = 4.0f;
    [SerializeField] private float _angleViewUp = 80.0f;
    [SerializeField] private float _angleViewDown = -80.0f;
    [SerializeField] private Vector2 _axiesFactor = new Vector2(2.35f, 1.15f);
    private float _currentDistance;
    private Vector3 _resultPosition;
    private Vector2 _angles;

    public override string Name => "camera";

    private void Start()
    {
    }
    
    private void Update()
    {
        const float AdditinalDistance = 0.25f;

        Vector3 startCirclePosition = _objectToFollow.transform.position;
        Vector3 normalToActor;
        Vector3 followPos = _objectToFollow.transform.position;

        if (Physics.Linecast(followPos, followPos + followPos.GetNormalTo(transform.position) * _selectedDistance, out RaycastHit rayCastHit))
        {
            _currentDistance = rayCastHit.distance - AdditinalDistance;
        }
        else
            _currentDistance = _selectedDistance;

        _angles.x -= Input.GetAxis("Mouse X") * _axiesFactor.x;
        _angles.y -= Input.GetAxis("Mouse Y") * _axiesFactor.y;
        _angles.y = Mathf.Clamp(_angles.y, _angleViewDown, _angleViewUp);

        startCirclePosition.x -= _currentDistance * Mathf.Cos(_angles.x * Mathf.Deg2Rad);
        startCirclePosition.z -= _currentDistance * Mathf.Sin(_angles.x * Mathf.Deg2Rad);

        normalToActor = startCirclePosition.GetNormalTo(followPos);

        startCirclePosition += normalToActor * Mathf.Cos(_angles.y * Mathf.Deg2Rad) * (_currentDistance) + normalToActor * _currentDistance;
        startCirclePosition.y += Mathf.Sin(_angles.y * Mathf.Deg2Rad) * _currentDistance;
       
        _resultPosition = startCirclePosition;
    }

    private void FixedUpdate()
    {
        transform.position = _resultPosition;
        transform.LookAt(_objectToFollow.transform);
    }

    public void SetDistance(float distance) => _selectedDistance = Mathf.Clamp(distance, _minDistance, _maxDistance);

    public void AddDistance(float distance) => _selectedDistance = Mathf.Clamp(_selectedDistance + distance, _minDistance, _maxDistance);

}