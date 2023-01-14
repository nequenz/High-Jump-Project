using System;
using UnityEngine;

public delegate void RaycastAction(RaycastHit hit);

public enum HookMode
{
    Swing = 1,
    PullOn = 2
}

public class RopeShooter : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private VisualRope _ropePrefab;
    [SerializeField] private Transform _from;
    [SerializeField] private Transform _hookTransform;
    [SerializeField] private float _maxDistanceToHook;

    private VisualRope _ropeRef;
    private Vector3 _sreenTarget;
    private RaycastHit _lastHit;
    private Ray _rayFromScreen;
    private bool _isHitted;

    public event Action Detected;
    public event Action Undetected;

    public float MaxDistanceToHook => _maxDistanceToHook;
    public Vector3 ScreenTarget => _sreenTarget;
    public RaycastHit LastHit => _lastHit;
    public Ray RayFromScreen => _rayFromScreen;
    public bool IsHitted => _isHitted;
    public Transform From => _from;
    public Transform Hook => _hookTransform;

    private void Start()
    {
        _ropeRef = Instantiate(_ropePrefab, transform.position, Quaternion.identity);
        _ropeRef.SetEnableMode(false);
    }

    private void Update()
    {
        SetScreenTargetPosition();

        bool isHitted = RaycastFromScreen(_sreenTarget);
            
        if (isHitted && _isHitted != isHitted)
            Detected?.Invoke();

        if (!isHitted && _isHitted != !isHitted)
            Undetected?.Invoke();

        _isHitted = isHitted;
    }

    private void SetScreenTargetPosition()
    {
        const float WidthCenterFactor = 2.0f;
        const float HeightCenterFactorOffseted = 1.5f; 

        _sreenTarget.x = Screen.width / WidthCenterFactor;
        _sreenTarget.y = Screen.height / HeightCenterFactorOffseted;
    }

    private bool RaycastFromScreen(Vector3 screenPoint)
    {
        _rayFromScreen = _camera.ScreenPointToRay(screenPoint);

        return Physics.Linecast(_rayFromScreen.origin, _rayFromScreen.origin + _rayFromScreen.direction * _maxDistanceToHook, out _lastHit);
    }

    public void ShootRope()
    {
        _hookTransform.position = _lastHit.point;

        _ropeRef.SetFrom(_from);
        _ropeRef.SetTo(_hookTransform);
        _ropeRef.SetEnableMode(true);
    }

    public void RemoveRope()
    {
        _ropeRef.SetEnableMode(false);
    }

    public bool IsRopeEnabled() => _ropeRef.IsEnabled;
}
