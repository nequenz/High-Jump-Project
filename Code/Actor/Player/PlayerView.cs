using UnityEngine;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour
{
    private StateMachine _stateMachine;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _stateMachine = GetComponent<StateMachine>();
    }

    private void Update()
    {
        
    }

    public void SetBool(int paramHash, bool isResult) => _animator.SetBool(paramHash,isResult);

}
