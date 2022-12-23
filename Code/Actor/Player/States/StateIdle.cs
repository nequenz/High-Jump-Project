using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : ActorState
{
    private PlayerEntry entry;

    public StateIdle(States id, object target,Transition[] transitions) : base(id, target, transitions)
    {
        entry = Target as PlayerEntry;
    }
}
