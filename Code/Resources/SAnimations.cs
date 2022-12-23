using UnityEngine;


public static class SAnimations 
{
    public const int NoState = -1;

    public static readonly int OnIdle = Animator.StringToHash("OnIdle");
    public static readonly int OnGround = Animator.StringToHash("OnGround");
    public static readonly int OnWalk = Animator.StringToHash("OnWalk");
    public static readonly int OnRun = Animator.StringToHash("OnRun");
    public static readonly int OnSprint = Animator.StringToHash("OnSprint");
    public static readonly int OnFall = Animator.StringToHash("OnFall");
    public static readonly int OnFastFall = Animator.StringToHash("OnFastFall");
    public static readonly int OnLanding2 = Animator.StringToHash("OnLanding2");
    public static readonly int OnWall = Animator.StringToHash("OnWall");
    public static readonly int OnSwing = Animator.StringToHash("OnSwing");
    public static readonly int OnJump2 = Animator.StringToHash("OnJump2");

    public static readonly int BaseLayer = Animator.StringToHash("Base Layer");


}
