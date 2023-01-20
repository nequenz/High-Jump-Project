using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class ActorView : MonoBehaviour
{
    [SerializeField] private Transform _meshTransform;
    private Animator _animator;

    public Transform MeshTrasform => _meshTransform;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        OnAwake();
    }

    protected virtual void OnAwake()
    {
    }

    public void Play(string stateName, int layerId, float normalizedTime) => _animator.Play(stateName,layerId, normalizedTime);

    public void Play(int stateNameHash, int layerId, float normalizedTime) => _animator.Play(stateNameHash, layerId, normalizedTime);

    public void SetBool(int paramHash, bool isResult) => _animator.SetBool(paramHash,isResult);

    public bool GetBool(int paramHash) => _animator.GetBool(paramHash);

}
