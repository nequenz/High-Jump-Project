

public class StateRun : ActorState
{
    private PlayerEntry entry;

    public StateRun(States id, object target) : base(id, target)
    {
        entry = target as PlayerEntry;
    }

    protected override void OnUpdate()
    {
    
    }
}