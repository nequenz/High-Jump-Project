using UnityEngine;

public static class VectorExtended
{
    public static Vector3 GetNormalTo( this Vector3 me, Vector3 otherVector ) => (otherVector - me) / Vector3.Distance(me, otherVector);

    public static Vector3 Get2DNormalTo( this Vector3 me, Vector3 otherVector )
    {
        Vector2 me2d = new Vector2(me.x, me.z);
        Vector2 otherVector2d = new Vector2(otherVector.x, otherVector.z);
        Vector2 normal2d = (otherVector2d - me2d) / Vector2.Distance(me2d, otherVector2d);

        return new Vector3(normal2d.x, 0.0f, normal2d.y);
    }

    public static float Get2DMagnitude(this Vector3 me) => new Vector2(me.x, me.z).magnitude;
}
