using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraEffector : MonoBehaviour
{
    [SerializeField] ActorPhysics _actorToFollow;
    [SerializeField] float _maxFieldOfView = 90.0f;
    [SerializeField] float _lerpSpeed = 0.75f;
    [SerializeField] float _atSpeedChange = 7.0f;
    private float _savedFov;
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Start()
    {
        _savedFov = _camera.fieldOfView;
    }

    private void Update()
    {
        const float MaxFov = 90.0f;
        float targetFov;

        if (_actorToFollow == null)
            return;

        targetFov = _actorToFollow.Velocity.Get2DMagnitude() > _atSpeedChange ? MaxFov : _savedFov;
        _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, targetFov, _lerpSpeed * Time.deltaTime);
    }
}
