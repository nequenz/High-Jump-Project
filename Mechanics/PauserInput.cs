using UnityEngine;

public class PauserInput : LocalInput
{
    public const int PauseActionID = 0;

    [SerializeField] private Pauser _pauser;

    public override int ID => 0;
    public KeyCode PauseKey { get; private set; } = KeyCode.Escape;

    private void Awake()
    {
        if (_pauser is null)
            return;

        AttachAction(PauseActionID, _pauser.Pause, KeyMode.Down, PauseKey);
    }

    private void Update()
    {
        HandleInput();
    }
}
