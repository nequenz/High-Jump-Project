


public static class GameStateExtended
{
    public static void AttachTransition<T>(this IState me, TransitionType type, TransitionPredicate predicate)
    {
        me.AttachTransition(new Transition(typeof(T), type, predicate));
    }
}