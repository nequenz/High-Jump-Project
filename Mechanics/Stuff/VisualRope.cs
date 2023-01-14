using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VisualRope : MonoBehaviour
{
    private Transform _from;
    private Transform _to;
    private LineRenderer _line;
    private float _maxLifeTime = 5.0f;
    private float _currentlifeTime;

    public Transform From => _from;
    public Transform To => _to;
    public bool IsEnabled => gameObject.activeInHierarchy;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        _line.positionCount = 2;
        _currentlifeTime = _maxLifeTime;
    }

    private void Update()
    {
        UpdateJoins();
    }

    private void Vanish()
    {
        Color vahishColor = _line.material.color;

        if (_currentlifeTime > 0.0f)
        {
            _currentlifeTime -= Time.deltaTime;
            vahishColor.a = (1 / _maxLifeTime) * _currentlifeTime;
            _line.startColor = vahishColor;
            _line.endColor = vahishColor;
        }

        if (_currentlifeTime <= 0.0f)
        {
            gameObject.SetActive(false);
            _currentlifeTime = _maxLifeTime;
        }
    }

    private void UpdateJoins()
    {
        const int FromIndex = 0;
        const int ToIndex = 1;

        _line.SetPosition(FromIndex, _from.position);
        _line.SetPosition(ToIndex, _to.position);
    }

    public void SetFrom(Transform from) => _from = from;

    public void SetTo(Transform to) => _to = to;

    public void SetEnableMode(bool isEnabled) => gameObject.SetActive(isEnabled);
}
