using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFovEffector : MonoBehaviour
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
        float targetFov;

        if (_actorToFollow == null)
            return;

        targetFov = _actorToFollow.VelocityValue > _atSpeedChange ? _maxFieldOfView : _savedFov;
        _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, targetFov, _lerpSpeed * Time.deltaTime);
    }
}
