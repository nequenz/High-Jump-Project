using UnityEngine;

[RequireComponent(typeof(CameraInput))]
public class CameraMoveEffector : MonoBehaviour
{
    [SerializeField] private ActorPhysics _actorToFollow;
    [SerializeField] private float _wiggleForceFactor = 10.0f;
    private CameraInput _camera;
    private float _angle = 0;

    private void Awake()
    {
        _camera = GetComponent<CameraInput>();
    }

    private void Update()
    {
        float actorFollowVelocity = _actorToFollow.VelocityXZValue;
        bool isActorOnGround = _actorToFollow.IsOnGroundChached;

        if(isActorOnGround && actorFollowVelocity > ActorPhysics.NoVelocity)
        {
            _angle += Time.deltaTime * _wiggleForceFactor;
            _camera.SetRotate(Quaternion.AngleAxis(Mathf.Sin(_angle), transform.right));

            if (_angle > Mathf.PI * 2)
                _angle = 0;
        }
        else if (actorFollowVelocity <= ActorPhysics.NoVelocity && _angle > 0.0f)
        {
            _angle = Mathf.Lerp(_angle, 0, Time.deltaTime * _wiggleForceFactor);
            _camera.SetRotate(Quaternion.AngleAxis(Mathf.Sin(_angle), transform.right));
        }
    }
}

