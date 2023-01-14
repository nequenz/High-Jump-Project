using UnityEngine;

public delegate bool UnityInputSignature(KeyCode key);

public enum KeyMode
{
    Hold,
    Down,
    Up,
}

public static class KeyCodes
{
    public static readonly UnityInputSignature[] BindedUnityInputMethods = {

        Input.GetKey,
        Input.GetKeyDown,
        Input.GetKeyUp,

    };

    private static KeyCode _playerForwardMove = KeyCode.W;
    private static KeyCode _playerBackwardMove = KeyCode.S;
    private static KeyCode _playerLeftMove = KeyCode.A;
    private static KeyCode _playerRightMove = KeyCode.D;
    private static KeyCode _playerJump = KeyCode.Space;
    private static KeyCode _playerSprint = KeyCode.LeftShift;
    private static KeyCode _useRope = KeyCode.E;
    private static KeyCode _useSlowMotionMode = KeyCode.LeftAlt;

    public static KeyCode PlayerForwardMove => _playerForwardMove;
    public static KeyCode PlayerBackwardMove => _playerBackwardMove;
    public static KeyCode PlayerLeftMove => _playerLeftMove;
    public static KeyCode PlayerRightMove => _playerRightMove;
    public static KeyCode PlayerJump => _playerJump;
    public static KeyCode PlayerSprint => _playerSprint;
    public static KeyCode UseRope => _useRope;
    public static KeyCode RopeShotModeUse => _useSlowMotionMode;
}