using UnityEngine;


public static class AnimationNames 
{
    public const int NoState = -1;

    public static readonly int ToIdle = Animator.StringToHash("ToIdle");
    public static readonly int ToWalk = Animator.StringToHash("ToWalk");
    public static readonly int ToRun = Animator.StringToHash("ToRun");
    public static readonly int ToSprint = Animator.StringToHash("ToSprint");
    public static readonly int ToFall = Animator.StringToHash("ToFall");
    public static readonly int ToFastFall = Animator.StringToHash("ToFastFall");
    public static readonly int ToLanding = Animator.StringToHash("ToLanding");
    public static readonly int ToWall = Animator.StringToHash("ToWall");
    public static readonly int ToSwing = Animator.StringToHash("ToSwing#0");
    public static readonly int ToJump = Animator.StringToHash("ToJump");
    public static readonly int ToJumpEnd = Animator.StringToHash("ToJumpEnd");

}
