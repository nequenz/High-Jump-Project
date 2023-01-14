using UnityEngine;

public static class PhysicsExtended
{
    public const float DefaultFixedDeltaTime = 0.02f;

    public static Vector3 SurfaceCast(Vector3 from, Vector3 to,Vector3 rightHandVector, float pitchValue, float rollValue)
    {
        Vector3 castVector = to - from;
        Vector3 result;

        castVector = Quaternion.AngleAxis(pitchValue, rightHandVector) * (castVector * (1.0f + (1 - Mathf.Cos(pitchValue * Mathf.Deg2Rad))));
        castVector = Quaternion.AngleAxis(rollValue, to - from) * castVector + from;

        result = to.GetNormalTo(castVector);

        bool isForwardVectorHitted = Physics.Linecast(from, to, out RaycastHit forwardVectorHit);
        bool isCastVectorHitted = Physics.Linecast(from, castVector, out RaycastHit castVectorHit);

        Debug.DrawLine(from, to, Color.black, 0.1f);
        Debug.DrawLine(from, castVector, Color.black, 0.1f);
        Debug.DrawLine(forwardVectorHit.point, castVectorHit.point, Color.red, 0.1f);

        if(isForwardVectorHitted && isCastVectorHitted)
            result = forwardVectorHit.point.GetNormalTo(castVectorHit.point);

        return result;
    }

    public static Vector3 SurfaceCastV2(Vector3 from,Vector3 to,float pitchValue, float rollValue)
    {
        float divPitchValue = pitchValue / 2;
        bool isUpHitted;
        bool isDownHitted;
        Vector3 castVector = to - from;
        Vector3 rightHandVector = new Vector3(castVector.z, 0, -castVector.x);//need fix
        Vector3 castVectorUp = Quaternion.AngleAxis(divPitchValue * -1, rightHandVector) * castVector;
        Vector3 castVectorDown = Quaternion.AngleAxis(divPitchValue, rightHandVector) * castVector;
        Vector3 resultVector;

        castVectorUp = Quaternion.AngleAxis(rollValue, castVector) * castVectorUp;
        castVectorDown = Quaternion.AngleAxis(rollValue, castVector) * castVectorDown;

        isUpHitted = Physics.Linecast(from,from + castVectorUp, out RaycastHit upHit);
        isDownHitted = Physics.Linecast(from, from + castVectorDown, out RaycastHit downHit);

        upHit.point = isUpHitted ? upHit.point : from + castVectorUp;
        downHit.point = isDownHitted ? downHit.point : from + castVectorDown;

        resultVector = downHit.point.GetNormalTo(upHit.point);

        return resultVector;
    }
}