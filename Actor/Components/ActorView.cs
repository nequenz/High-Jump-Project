using UnityEngine;

[RequireComponent(typeof(ActorPhysics))]
[RequireComponent(typeof(Animator))]
public abstract class ActorView : MonoBehaviour
{
    [SerializeField] private Transform _meshTransform;
    private ActorPhysics _actorPhysics;
    private Animator _animator;

    public ActorPhysics Physics => _actorPhysics;
    public Transform MeshTrasform => _meshTransform;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _actorPhysics = GetComponent<ActorPhysics>();
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
