using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static Vector2 Vector2fromAngle(float angle)
    {
        angle += 90f;
        angle *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    public static float AngleFromVector2(Vector2 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
    }
}
