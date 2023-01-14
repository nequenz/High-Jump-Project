using UnityEngine;
using UnityEngine.UI;

public class RopeShooterView : MonoBehaviour
{
    [SerializeField] private RopeShooter _ropeShooter;
    [SerializeField] private Image _targetHookImage;
    [SerializeField] private Sprite _spriteEnabled;
    [SerializeField] private Sprite _spriteDisabled;
    private float _animationValue = 0;
    private bool _isAnimating = false;

    private void Update()
    {
        if (_ropeShooter is null)
            return;

        _targetHookImage.rectTransform.position = _ropeShooter.ScreenTarget;

        if(_isAnimating)
            AnimateScale(); 
    }

    private void OnEnable()
    {
        if (_ropeShooter is null || _targetHookImage is null)
            enabled = false;

        _ropeShooter.Detected += OnDetectObject;
        _ropeShooter.Undetected += OnUndetectObject;
    }

    private void OnDisable()
    {
        _ropeShooter.Detected -= OnDetectObject;
        _ropeShooter.Undetected -= OnUndetectObject;
    }

    private void OnDetectObject()
    {
        UpdateSprite(_spriteEnabled);
        _isAnimating = true;
    }

    private void OnUndetectObject()
    {
        UpdateSprite(_spriteDisabled);
        _isAnimating = false;
        ResetScale();
    }

    private void UpdateSprite(Sprite sprite)
    {
        if (_targetHookImage.sprite != sprite)
            _targetHookImage.sprite = sprite;
    }

    private void AnimateScale()
    {
        const float AnimateSpeed = 2;
        Vector3 scaleOffset = new Vector3(0.5f,0.5f,0.5f);
        Vector3 _one = Vector3.one;

        _animationValue += Time.unscaledDeltaTime * AnimateSpeed;

        if (_animationValue >= Mathf.PI / 2)
            _animationValue *= -1;

        _one *= Mathf.Cos(_animationValue) * scaleOffset.x;
        _one += scaleOffset;
        _targetHookImage.rectTransform.localScale = _one;
    }

    private void ResetScale()
    {
        _targetHookImage.rectTransform.localScale = Vector3.one;
        _animationValue = 0.0f;
    }
}
